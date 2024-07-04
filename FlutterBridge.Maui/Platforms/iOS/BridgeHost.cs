using FlutterBinding;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Models;
using Foundation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UIKit;

namespace FlutterBridge.Maui
{
    public partial class BridgeHost : IDisposable
    {
        /// <summary>
        /// The platform side stream handler has two methods, <see cref="OnListen"/> and <see cref="OnCancel"/>, which are invoked
        /// whenever the number of listeners to the Dart stream goes from zero to one and back, respectively.
        /// This can happen multiple times. The stream handler implementation is supposed to start pouring events into the event sink
        /// when the former is called, and stop when the latter is called.
        /// In addition, it should pause when the ambient app component is not running.
        /// </summary>
        /// <seealso href="https://medium.com/flutter/flutter-platform-channels-ce7f540a104e"/>
        internal class StreamHandler : NSObject, IFlutterStreamHandler
        {
            BridgeHost _bridge;
            FlutterEventSink _events;

            public StreamHandler(BridgeHost bridge)
            {
                _bridge = bridge;
            }

            /// <summary>
            /// Tears down an event stream.
            /// Invoked when the last listener is deregistered from the Stream associated to this channel on the Flutter side.
            /// </summary>
            /// <param name="arguments">Arguments for the stream.</param>
            /// <returns>A <see cref="FlutterError"/> instance, if teardown fails.</returns>
            public FlutterError OnCancel(NSObject arguments)
            {
                _events = null;
                return null;
            }

            /// <summary>
            /// Sets up an event stream and begin emitting events.
            /// Invoked when the first listener is registered with the Stream associated to this channel on the Flutter side.
            /// </summary>
            /// <param name="arguments">Arguments for the stream.</param>
            /// <param name="events">
            /// A callback to asynchronously emit events.
            /// Invoke the callback with a <see cref="FlutterError"/> to emit an error event.
            /// Invoke the callback with <see cref="ConstantsEx.FlutterEndOfEventStream"/> to indicate that no more events will be emitted.
            /// Any other value, including null, are emitted as successful events.
            /// </param>
            /// <returns>A <see cref="FlutterError"/> instance, if setup fails.</returns>
            public FlutterError OnListen(NSObject arguments, FlutterEventSink events)
            {
                _events = events;
                return null;
            }

            public FlutterEventSink EventSink => _events;
        }

        readonly FlutterMethodChannel _methodChannelIncoming;
        readonly FlutterEventChannel _eventChannel;
        readonly StreamHandler _streamHandler;

        WebSocketService? _socketService;
        bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="BridgeHost"/> class.
        /// </summary>
        public BridgeHost(FlutterEngine engine) : this(engine, FlutterBridgeMode.PlatformChannel)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BridgeHost"/> class
        /// specifying how platform code and Flutter code communicate.
        /// </summary>
        public BridgeHost(FlutterEngine engine, FlutterBridgeMode mode)
        {
            // Create the named channel for communicating with Flutter module using asynchronous method calls
            // NOTE: This channel is used to RECEIVE messages/requests FROM Flutter
            _methodChannelIncoming = FlutterMethodChannel.Create("flutterbridge.incoming", engine.BinaryMessenger);
            _methodChannelIncoming.SetMethodCallHandler(HandleMethodCall);

            // Create the named channel for communicating with Flutter module using event streams
            // NOTE: This channel is used to SEND messages/notifications TO Flutter
            // An event channel is a specialized platform channel intended for the use case of exposing platform events to Flutter as a Dart stream.
            // The Flutter SDK currently has no support for the symmetrical case of exposing Dart streams to platform code, though that could be built, if the need arises.
            // see: https://medium.com/flutter/flutter-platform-channels-ce7f540a104e

            _streamHandler = new StreamHandler(this);
            _eventChannel = FlutterEventChannel.Create("flutterbridge.outgoing", engine.BinaryMessenger);
            _eventChannel.SetStreamHandler(_streamHandler);

            Mode = mode;

            BridgeRuntime.OnBridgeEvent += OnHostBridgeEvent;

            if (Mode == FlutterBridgeMode.WebSocket)
            {
                _socketService = new WebSocketService();
            }
        }

        /// <summary>
        /// Releases all resources used by this object.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
                return;

            BridgeRuntime.OnBridgeEvent -= OnHostBridgeEvent;

            _methodChannelIncoming.Dispose();
            _eventChannel.Dispose();
            _streamHandler.Dispose();

            if (Mode == FlutterBridgeMode.WebSocket)
            {
                _socketService?.Dispose();
                _socketService = null;
            }

            _disposed = true;
        }

        /// <summary>
        /// Specifies how platform code and Flutter code communicate.
        /// </summary>
        public FlutterBridgeMode Mode { get; }

        private void HandleMethodCall(FlutterMethodCall call, FlutterResult callback)
        {
            // Return an error if Flutter is invoking method calls through method channel
            // when bridge is configured for WebSocket communication
            var operationKey = call.Method;
            if (string.IsNullOrEmpty(operationKey) || Mode == FlutterBridgeMode.WebSocket)
            {
                callback(ConstantsEx.FlutterMethodNotImplemented);
                return;
            }

            // Right now this handler is called just once at application startup
            // when Flutter module tries to detect if it's running
            // embedded into a native Xamarin app or as a standalone app
            if (operationKey == "checkEmbedded")
            {
                callback(NSObject.FromObject(true));
                return;
            }

            // Extract target method information from MethodCall.Method
            long requestId = 0;
            NSObject dartReturnValue;
            try
            {
                if (call.Arguments is NSDictionary dartArguments)
                {
                    requestId = (long)(dartArguments["requestId"] as NSNumber);
                }
                dartReturnValue = FlutterInterop.ToMethodChannelResult(0);
            }
            catch (Exception ex)
            {
                callback(FlutterError.Create(BridgeErrorCode.OperationNotImplemented.ToString(), ex.Message, null));
                return;
            }

            // Send an empty - successful - response to immediately free Flutter thread
            callback(dartReturnValue);

            nint[] taskId = new nint[1];

            taskId[0] = UIApplication.SharedApplication.BeginBackgroundTask(() =>
            {
                var error = new BridgeException(BridgeErrorCode.OperationCanceled);
                MainThread.BeginInvokeOnMainThread(() => SendError(requestId, operationKey, error));
                UIApplication.SharedApplication.EndBackgroundTask(taskId[0]);
            });

            if (requestId > 0 && !string.IsNullOrEmpty(operationKey))
            {
                // Run the call in Background
                Task.Run(() =>
                {
                    BackgroundHandleMethodCall(requestId, operationKey, call);
                    UIApplication.SharedApplication.EndBackgroundTask(taskId[0]);
                });
            }
        }

        private void BackgroundHandleMethodCall(long requestId, string operationKey, FlutterMethodCall call)
        {
            var operation = BridgeRuntime.GetOperation(operationKey);
            if (operation == null)
            {
                SendError(requestId, operationKey, new BridgeException(BridgeErrorCode.OperationNotImplemented));
                return;
            }

            var dartArguments = call.Arguments as NSDictionary;
            var parametersCount = operation.ParametersCount;
            if (parametersCount > 0 && dartArguments == null)
            {
                SendError(requestId, operationKey, new BridgeException(BridgeErrorCode.OperationArgumentsCountMismatch));
                return;
            }

            var arguments = new object?[parametersCount];
            try
            {
                for (int i = 0; i < parametersCount; i++)
                {
                    ParameterInfo param = operation.Parameters![i];
                    var paramType = param.IsOut || param.ParameterType.IsByRef
                        ? param.ParameterType.GetElementType()!
                        : param.ParameterType;
                    var paramName = new NSString(param.Name!);

                    object? value = null;
                    if (dartArguments!.ContainsKey(paramName))
                    {
                        var argumentValue = dartArguments[paramName];
                        if (argumentValue != null)
                        {
                            if (argumentValue is FlutterStandardTypedData argumentTypedData)
                            {
                                value = argumentTypedData.Data.ToByteArray().ToProtoObject(paramType);
                            }
                            else
                            {
                                value = argumentValue.ToObject(paramType);
                            }
                        }
                    }
                    else if (param.HasDefaultValue)
                    {
                        value = param.DefaultValue;
                    }
                    else
                    {
                        SendError(requestId, operationKey, new BridgeException(BridgeErrorCode.OperationArgumentsInvalid));
                        return;
                    }

                    arguments[i] = value;
                }
            }
            catch (Exception)
            {
                SendError(requestId, operationKey, new BridgeException(BridgeErrorCode.OperationArgumentsParsingError));
                return;
            }

            var result = BridgeRuntime.Run(operation, arguments);
            if (result.Error != null)
            {
                if (result.Error is BridgeException flutterException)
                {
                    SendError(requestId, operationKey, flutterException);
                }
                else
                {
                    //In case of an unhandled exception, send to Flutter a verbose error message for better diagnostic
                    var error = new BridgeException(BridgeErrorCode.OperationFailed, result.Error.ToStringCleared(), result.Error);
                    SendError(requestId, operationKey, error);
                }
            }
            else
            {
                SendResult(requestId, operationKey, result.Result);
            }
        }

        private void OnHostBridgeEvent(object? sender, BridgeEventArgs e)
        {
            // Prevent dispatching events to Flutter through event channel
            // if bridge is configured for WebSocket communication
            if (Mode == FlutterBridgeMode.WebSocket)
                return;

            var eventInfo = new BridgeEventInfo
            {
                ServiceName = e.ServiceName,
                EventName = e.EventName.FirstCharLower(),
                EventData = e.EventData.ToProtoBytes(),
            };

            NSObject eventValue = FlutterInterop.ToMethodChannelResult(eventInfo);

            // As stated here https://docs.microsoft.com/en-US/xamarin/essentials/main-thread#determining-if-code-is-running-on-the-main-thread:
            // The platform implementations of BeginInvokeOnMainThread themselves check if the call is made on the main thread. 
            // There is very little performance penalty if you call BeginInvokeOnMainThread when it's not really necessary.

            if (!MainThread.IsMainThread)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        _streamHandler.EventSink?.Invoke(eventValue);
                    }
                    catch (Exception ex)
                    {
                        // TODO: Properly log any error
                        Console.WriteLine(ex.Message);
                    }
                });
            }
            else
            {
                try
                {
                    _streamHandler.EventSink?.Invoke(eventValue);
                }
                catch (Exception ex)
                {
                    // TODO: Properly log any error
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void SendResult(long requestId, string operationKey, object? result)
        {
            var message = new NSDictionary();
            message["requestId"] = NSObject.FromObject(requestId);
            message["result"] = result.ToProtoBytes().ToByteData();

            Console.WriteLine("Sending result to Flutter...");
            MainThread.BeginInvokeOnMainThread(() => _methodChannelIncoming.InvokeMethod("result", message));
        }

        private void SendError(long requestId, string operationKey, BridgeException exception)
        {
            var message = new NSDictionary();
            message["requestId"] = NSObject.FromObject(requestId);
            message["exception"] = exception.ToProtoBytes().ToByteData();

            Console.WriteLine("Sending error to Flutter...");
            MainThread.BeginInvokeOnMainThread(() => _methodChannelIncoming.InvokeMethod("error", message));
        }
    }
}
