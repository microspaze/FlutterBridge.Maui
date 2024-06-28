import 'dart:async';
import 'dart:developer';
import 'package:flutter/services.dart';
import 'package:flutter/widgets.dart';
import 'package:synchronized/synchronized.dart';
import 'package:web_socket_channel/io.dart';
import 'package:web_socket_channel/status.dart' as status;
import 'package:web_socket_channel/web_socket_channel.dart';

import 'package:flutter_maui_bridge/proto/flutter_maui_bridge.pb.dart';

///
/// The bridge communication type with native side.
///
enum FlutterBridgeMode {
  PlatformChannel,
  WebSocket,
}

///
/// The configuration used by the [FlutterBridge].
/// Setup this before running the flutter application.
/// [main.dart] --> void main()
///
class FlutterBridgeConfig {
  static FlutterBridgeMode mode = FlutterBridgeMode.PlatformChannel;
}

class FlutterBridge {
  // Events from native side (MAUI)
  static const EventChannel _events = EventChannel('flutterbridge.outgoing');

  // The real event stream from navive side
  static final Stream<BridgeEventInfo?> _channelEvent =
      _events.receiveBroadcastStream().map(_mapEvent);

  // The event stream exposed to all the services
  final Stream<BridgeEventInfo?>
      _netEvent; // = _events.receiveBroadcastStream().map(_mapEvent);

  //
  // Filter the bridge event stream
  // using a specific instanceId, event
  //
  Stream<List<int>> events({
    required String serviceName,
    required String eventName,
  }) {
    // Filter the stream by instanceId and event name.
    return _netEvent
        .where((e) => e!.serviceName == serviceName && e.eventName == eventName)
        .map((e) => e!.eventData);
  }

  static final FlutterBridge _instance =
      FlutterBridge._internal(FlutterBridgeConfig.mode);

  FlutterBridge._internal(FlutterBridgeMode mode)
      : invokeMethod = (buildMode == _BuildMode.release)
            ? _invokeOnChannel
            : (mode == FlutterBridgeMode.WebSocket)
                ? _invokeOnSocket
                : _invokeOnChannel,
        _netEvent = (buildMode == _BuildMode.release)
            ? _channelEvent
            : (mode == FlutterBridgeMode.WebSocket)
                ? _WebSocketChannel().events
                : _channelEvent;

  factory FlutterBridge() => _instance;

  /// Invoke the message on the channel
  final Future<Map<String, dynamic>> Function({
    required String? service,
    required String? operation,
    required Map<String, dynamic>? arguments,
  }) invokeMethod;

  static Future<Map<String, dynamic>> _invokeOnChannel({
    required String? service,
    required String? operation,
    required Map<String, dynamic>? arguments,
  }) {
    print(
      "Invoking on platform channel $operation on $service: build mode:$buildMode",
    );
    return _PlatformChannel().invokeMethod(
      service: service,
      operation: operation,
      arguments: arguments,
    );
  }

  static Future<Map<String, dynamic>> _invokeOnSocket({
    required String? service,
    required String? operation,
    required Map<String, dynamic>? arguments,
  }) {
    print(
      "Invoking on socket $operation on $service: build mode:$buildMode",
    );
    return _WebSocketChannel().invokeMethod(
      service: service,
      operation: operation,
      arguments: arguments,
    );
  }

  ///
  /// Decoding events function
  ///
  static BridgeEventInfo? _mapEvent(dynamic event) {
    try {
      return BridgeEventInfo.fromBuffer(event as Uint8List);
    } on Exception catch (ex) {
      print("Error decoding event: $ex");
      return null;
    }
  }

  @mustCallSuper
  void dispose() {
    // Release debug socket resources
    if (invokeMethod == _invokeOnSocket) {
      _WebSocketChannel().dispose();
    }
  }
}

class _PlatformChannel {
  static _PlatformChannel _instance = _PlatformChannel._internal();

  factory _PlatformChannel() => _instance;

  // Send request id
  int _uniqueId = 0;
  bool? _appEmbedded = null;
  Lock _sendLock = new Lock();

  // All the request to be satisfied by channel.
  Map<int, Completer<Map<String, dynamic>>> _sendRequestMap = {};

  // The real communication channel with native platform
  final _platformChannel = MethodChannel('flutterbridge.incoming');

  Future<bool> _isAppEmbedded() async {
    try {
      if (_appEmbedded != null) {
        return _appEmbedded!;
      }
      _appEmbedded = await _platformChannel.invokeMethod<bool>("checkEmbedded");
      return _appEmbedded == true;
    } catch (ex) {
      return false;
    }
  }

  _PlatformChannel._internal() {
    _platformChannel.setMethodCallHandler(_onMessageReceived);
  }

  _releaseMemory() {
    try {
      _sendRequestMap.clear();
    } catch (ex) {}
  }

  static const _emptyString = "";

  /// How manage data reception from native channel.
  Future<dynamic> _onMessageReceived(MethodCall call) async {
    // Manage message received
    try {
      BridgeMessageInfo msg =
          BridgeMessageInfo.fromBuffer(call.arguments as Uint8List);

      // Insert the response the the map
      await _sendLock.synchronized(() {
        var requestId = msg.requestId;
        if (_sendRequestMap.containsKey(requestId)) {
          // Invoke the task completion
          // var isFailed = msg.errorCode != null && msg.errorCode.isNotEmpty;
          // if (isFailed && msg.exception != null) {
          //   Completer<Map<String, dynamic>>? request =
          //       _sendRequestMap[requestId];
          //   var exception = PlatformOperationException.fromJsonDynamic(
          //     msg.exception as Map<String, dynamic>,
          //   );
          //   request?.completeError(exception);
          // }
          _sendRequestMap.remove(requestId);
        }
      });
    } catch (e) {
      // Error during deserialization
      print(
        "flutter_MAUI_debug1: error during _onMessageReceived deserialization." +
            e.toString(),
      );
    }

    return _emptyString;
  }

  Future<Map<String, dynamic>> invokeMethod({
    required String? service,
    required String? operation,
    required Map<String, dynamic>? arguments,
  }) {
    final Completer<Map<String, dynamic>> completer =
        new Completer<Map<String, dynamic>>();

    _sendLock.synchronized(
      () async {
        int sendRequestId = ++_uniqueId;
        String operationKey = "$service.$operation";

        try {
          // Save the request
          _sendRequestMap.putIfAbsent(
            sendRequestId,
            () => completer,
          );

          // Serialize all the args as protobuf bytes
          final Map<String, dynamic>? args = arguments != null
              ? arguments!.map(
                  (argName, value) => MapEntry(argName, value.writeToBuffer()))
              : Map<String, dynamic>();
          args!["requestId"] = sendRequestId;

          // Send to platform channel
          await _platformChannel.invokeMethod(
            operationKey,
            args,
          );
        } on MissingPluginException {
          _sendRequestMap.remove(sendRequestId);

          bool isAppEmbedded = await _isAppEmbedded();
          if (isAppEmbedded) {
            // Invalid call in embedded app
            completer.completeError(Exception(
              "Flutter is running as an EMBEDDED module inside your MAUI app, but your MAUI project have the FlutnetBrigde configuration set to ${FlutterBridgeMode.WebSocket}.\n"
              "If you want to run your Flutter project as a STANDALONE application, use your preferred Flutter IDE (like Visual Studio Code).\n"
              "Otherwise configure your MAUI project to use ${FlutterBridgeMode.PlatformChannel}.\n"
              "Ensure to have the same FlutnetBrigde configuration for both Flutter and MAUI project.",
            ));
          } else {
            // The user have run flutter using visual studio code, but the configuration cannot be BridgeMode.PlatformChannel
            completer.completeError(Exception(
              "Flutter is running as a STANDALONE application, so the FlutnetBrigde configuration must be ${FlutterBridgeMode.WebSocket}.\n"
              "Set 'FlutterBridgeConfig.mode = ${FlutterBridgeMode.WebSocket}' in your Flutter project.\n"
              "Remember to start your MAUI project with the same FlutterBridgeMode configuration.",
            ));
          }
        } on Exception catch (ex) {
          debugPrint("Error during invokeMethod on platform channel");
          _sendRequestMap.remove(sendRequestId);
          // Error during send
          completer.completeError(ex);
        }
      },
    );

    return completer.future;
  }

  @mustCallSuper
  void dispose() {
    _sendLock.synchronized(() {
      _releaseMemory();
    });
  }
}

class _WebSocketChannel {
  static _WebSocketChannel? _instance;

  final StreamController<BridgeEventInfo> _eventsController;
  final Stream<BridgeEventInfo> _eventsOut;
  final Sink<BridgeEventInfo> _eventsIn;

  Stream<BridgeEventInfo> get events => _eventsOut;

  _WebSocketChannel._internal(
      this._eventsController, this._eventsIn, this._eventsOut) {
    _sendLock.synchronized(() async {
      // Wait until the connection open
      while (_socketChannelConnected == false) {
        try {
          await _autoConnect(
            delay: const Duration(seconds: 1),
            forceOpen: true,
          );
        } catch (ex) {
          // Error during connection opening
          debugPrint(ex.toString());
        }
      }
    });
  }

  factory _WebSocketChannel() {
    if (_instance == null) {
      StreamController<BridgeEventInfo> controller =
          StreamController<BridgeEventInfo>();
      Stream<BridgeEventInfo> outEvent = controller.stream.asBroadcastStream();
      _instance =
          _WebSocketChannel._internal(controller, controller.sink, outEvent);
    }
    return _instance!;
  }

  // Dispose state
  bool _disposed = false;

  ///
  /// The url user for the connection
  ///
  String _url = "ws://127.0.0.1:12345/flutter";

  //
  // Channel used to invoke methods from Flutter to web socket native backend application.
  //
  WebSocketChannel? _socketChannel;

  // Status of the debug connection
  bool _socketChannelConnected = false;

  // Send request id
  int _uniqueId = 0;

  Lock _sendLock = new Lock();

  ///
  /// All the request to be satisfied by debug WEB SOCKET.
  ///
  Map<int, Completer<Map<String, dynamic>>> _sendRequestMap = {};

  ///
  /// All message sended to debug server
  /// that wait a respose
  ///
  Map<int, String> _outboxMessages = {};

  ///
  /// Oopen the connection resending all the queued messages with no response.
  ///
  Future<void> _autoConnect(
      {required Duration delay, bool forceOpen = false}) async {
    // * If disposed we release the memory
    if (_disposed) {
      await _closeConnection();
      _socketChannelConnected = false;
      await _releaseMemory();
    }
    // * If the connectin is open, but not message: close the connection.
    else if (_outboxMessages.length <= 0 &&
        _socketChannelConnected == true &&
        forceOpen == false) {
      await _closeConnection();
      _socketChannelConnected = false;
      await _releaseMemory();
    }
    // * Reopen the connection
    else if ((_outboxMessages.length > 0 && _socketChannelConnected == false) ||
        (forceOpen == true && _socketChannelConnected == false)) {
      // Aspetto un po prima di collegarmi
      await Future.delayed(delay);

      //* --------------------------------------------------------------
      //* IOWebSocketChannel.connect("ws://127.0.0.1:12345/flutter");
      //* OPEN THE CONNECCTION
      //* --------------------------------------------------------------
      _socketChannel = IOWebSocketChannel.connect(this._url);
      _socketChannel!.stream.listen(
        _onMessageReceived,
        cancelOnError: false,
        onDone: _onConnectionClosed,
        //! in caso di erroe sull'apertura viene emesso l'evento qui
        onError: _onConnectionError,
      );

      //* IMPORTANT NOTE: the connection never fail during the opening call.
      //* Only after that will be invoked the error event.
      _socketChannelConnected = true;

      // Se sono connesso provo ad inviare i messaggi
      if (_socketChannelConnected) {
        try {
          //* Try to resend all the append messages (IN SORT ORDER)
          List<int> sortedRequests = _outboxMessages.keys.toList()..sort();

          sortedRequests.forEach((reqId) {
            String? msg = _outboxMessages[reqId];

            _socketChannel!.sink.add(msg);
          });
        } catch (ex) {
          debugPrint("Error sending messages");
          _socketChannelConnected = false;
          _closeConnection();
        }
      } else {
        //! Error after N try
        throw Exception("Error opening channel!");
      }
    }
  }

  Future _closeConnection() async {
    try {
      await _socketChannel?.sink?.close(status.normalClosure);
    } catch (ex) {}
  }

  /// Release all the resources.
  _releaseMemory() async {
    // Try to resend all the append messages (IN SORT ORDER)
    List<int> sortedRequests = _sendRequestMap.keys.toList()..sort();

    sortedRequests.forEach((reqId) {
      _sendRequestMap[reqId]?.completeError(
        Exception("Connection closed by client."),
      );
    });

    try {
      _sendRequestMap.clear();
    } catch (ex) {}

    try {
      _outboxMessages.clear();
    } catch (ex) {}

    _eventsController.close();
  }

  /// Connection close event.
  Future _onConnectionClosed() async {
    print("Connection closed.");
    await _sendLock.synchronized(() async {
      _socketChannelConnected = false;

      //* ----------------------------------------------------------------
      //* Wait until the connection open (IF THIS OBJECT IS NOT DISPOSED)
      //* ----------------------------------------------------------------
      while (_socketChannelConnected == false && _disposed == false) {
        try {
          print("Restoring the connection....");
          await _autoConnect(
            delay: const Duration(seconds: 1),
            forceOpen: true,
          );
        } catch (ex) {
          // Error during connection opening
          debugPrint(ex.toString());
        }
      }
    });
  }

  /// Connection error handler.
  Future _onConnectionError(dynamic error, dynamic stacktrace) async {
    print("Connection error: closing the connection.");
    await _sendLock.synchronized(() {
      _socketChannelConnected = false;
      try {
        if (error is WebSocketChannelException) {
          log(error.message.toString());
        } else {
          log(error.toString());
        }
      } catch (ex) {}
    });
  }

  /// How manage data reception from websocket.
  void _onMessageReceived(dynamic socketMessage) async {
    if (socketMessage is Uint8List) {
      // Manage message received
      try {
        BridgeMessageInfo msg = BridgeMessageInfo.fromBuffer(socketMessage);

        // Handlig for event
        if (msg.event != null) {
          _eventsIn.add(msg.event);
        }

        // Deserialize the real application message
        //FNetMessage result = FNetSerializer.deserialize(msg.fnetMessage);

        // Insert the response the the map
        await _sendLock.synchronized(() {
          var requestId = msg.requestId;
          if (_outboxMessages.containsKey(requestId)) {
            _outboxMessages.remove(requestId);
          }

          if (_sendRequestMap.containsKey(requestId)) {
            // Invoke the task completion
            // var isFailed = msg.errorCode != null && msg.errorCode!.isNotEmpty;
            // if (isFailed && msg.exception != null) {
            //   Completer<Map<String, dynamic>>? request =
            //       _sendRequestMap[requestId];
            //   var exception = PlatformOperationException.fromJsonDynamic(
            //     msg.exception as Map<String, dynamic>,
            //   );
            //   request?.completeError(exception);
            // }
            _sendRequestMap.remove(requestId);
          }
        });
      } catch (e) {
        // Error during deserialization
        print(
          "flutter_MAUI_debug2: error during _onMessageReceived deserialization." +
              e.toString(),
        );
      }
    } else {
      // Message not managed: protocol debug error.
    }
  }

  Future<Map<String, dynamic>> invokeMethod({
    required String? service,
    required String? operation,
    required Map<String, dynamic>? arguments,
  }) {
    final Completer<Map<String, dynamic>> completer =
        new Completer<Map<String, dynamic>>();

    _sendLock.synchronized(
      () async {
        int sendRequestId = ++_uniqueId;
        String operationKey = "$service.$operation";

        try {
          final Map<String, Uint8List>? args = arguments != null
              ? arguments!.map(
                  (argName, value) => MapEntry(argName, value.writeToBuffer()))
              : null;

          final BridgeMessageInfo debugMessage = BridgeMessageInfo(
            requestId: sendRequestId,
            operationKey: operationKey,
            arguments: args,
          );

          // Save the request
          _sendRequestMap.putIfAbsent(
            sendRequestId,
            () => completer,
          );
          _outboxMessages.putIfAbsent(
            sendRequestId,
            () => "protoDegubMessage",
          );

          // Wait until the connection open
          while (_socketChannelConnected == false) {
            try {
              await _autoConnect(
                delay: const Duration(seconds: 1),
                forceOpen: true,
              );
            } catch (ex) {
              // Error during connection opening
              debugPrint(ex.toString());
            }
          }

          // Send data using network
          _socketChannel!.sink.add(debugMessage.writeToBuffer());
        } catch (ex) {
          if (ex is WebSocketChannelException) {}
          debugPrint("Error during invokeMethod on debug channel");
          // Error during send
          completer.completeError(ex);
        }
      },
    );

    return completer.future;
  }

  @mustCallSuper
  void dispose() {
    _sendLock.synchronized(() {
      _disposed = true;
      _closeConnection();
      _socketChannelConnected = false;
      _releaseMemory();
    });
  }
}

enum _BuildMode {
  release,
  debug,
  profile,
}

_BuildMode buildMode = (() {
  if (const bool.fromEnvironment('dart.vm.product')) {
    return _BuildMode.release;
  }
  var result = _BuildMode.profile;
  assert(() {
    result = _BuildMode.debug;
    return true;
  }());
  return result;
}());
