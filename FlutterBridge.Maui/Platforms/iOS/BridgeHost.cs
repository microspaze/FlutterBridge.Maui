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
        readonly FlutterMethodChannel _methodChannelTest;
        readonly FlutterEventChannel _eventChannel;
        readonly StreamHandler _streamHandler;

        WebSocketService _socketService;
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
            _methodChannelIncoming = FlutterMethodChannel.Create("flutnetbridge.incoming", engine.BinaryMessenger);
            _methodChannelIncoming.SetMethodCallHandler(HandleMethodCall);

            // Create a second named channel for diagnostic use only.
            // This channel is used, for example, to test if Flutter module is running
            // embedded into a native Xamarin app or as a standalone app
            _methodChannelTest = FlutterMethodChannel.Create("flutnetbridge.support", engine.BinaryMessenger);
            _methodChannelTest.SetMethodCallHandler(HandleMethodCallTest);

            // Create the named channel for communicating with Flutter module using event streams
            // NOTE: This channel is used to SEND messages/notifications TO Flutter

            // An event channel is a specialized platform channel intended for the use case of exposing platform events to Flutter as a Dart stream.
            // The Flutter SDK currently has no support for the symmetrical case of exposing Dart streams to platform code, though that could be built, if the need arises.
            // see: https://medium.com/flutter/flutter-platform-channels-ce7f540a104e

            _streamHandler = new StreamHandler(this);
            _eventChannel = FlutterEventChannel.Create("flutnetbridge.outgoing", engine.BinaryMessenger);
            _eventChannel.SetStreamHandler(_streamHandler);

            Mode = mode;

            BridgeRuntime.OnBridgeEvent += OnBridgeEvent;

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

            BridgeRuntime.OnBridgeEvent -= OnBridgeEvent;

            _methodChannelIncoming.Dispose();
            _methodChannelTest.Dispose();
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

        private void HandleMethodCallTest(FlutterMethodCall call, FlutterResult callback)
        {
            if (call.Method == "FlutterBridgeMode")
            {
                switch (Mode)
                {
                    case FlutterBridgeMode.PlatformChannel:
                        callback(NSObject.FromObject("PlatformChannel"));
                        break;
                    case FlutterBridgeMode.WebSocket:
                        callback(NSObject.FromObject("WebSocket"));
                        break;
                }
            }
            else
            {
                // Right now this handler is called just once at application startup
                // when Flutter module tries to detect if it's running
                // embedded into a native Xamarin app or as a standalone app
                callback(NSObject.FromObject("ok"));
            }
        }

        private void HandleMethodCall(FlutterMethodCall call, FlutterResult callback)
        {
            // Return an error if Flutter is invoking method calls through method channel
            // when bridge is configured for WebSocket communication
            if (Mode == FlutterBridgeMode.WebSocket)
            {
                callback(ConstantsEx.FlutterMethodNotImplemented);
                return;
            }

            // Extract target method information from MethodCall.Method
            BridgeMethodInfo methodInfo;
            NSObject dartReturnValue;
            try
            {
                methodInfo = JsonConvert.DeserializeObject<BridgeMethodInfo>(call.Method, FlutterInterop.JsonSerializerSettings);
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
                MainThread.BeginInvokeOnMainThread(() => SendError(methodInfo, error));
                UIApplication.SharedApplication.EndBackgroundTask(taskId[0]);
            });

            // Run the call in Background
            Task.Run(() =>
            {
                BackgroundHandleMethodCall(methodInfo, call);
                UIApplication.SharedApplication.EndBackgroundTask(taskId[0]);
            });
        }

        private void BackgroundHandleMethodCall(BridgeMethodInfo methodInfo, FlutterMethodCall call)
        {
            BridgeOperationInfo operation;
            try
            {
                operation = BridgeRuntime.GetOperation(methodInfo.Instance, methodInfo.Operation);
            }
            catch (Exception)
            {
                SendError(methodInfo, new BridgeException(BridgeErrorCode.OperationNotImplemented));
                return;
            }

            NSDictionary dartArguments = call.Arguments as NSDictionary;
            if (operation.Parameters.Length > 0 && dartArguments == null)
            {
                SendError(methodInfo, new BridgeException(BridgeErrorCode.OperationArgumentsCountMismatch));
                return;
            }

            object[] arguments = new object[operation.Parameters.Length];
            try
            {
                for (int i = 0; i < operation.Parameters.Length; i++)
                {
                    ParameterInfo param = operation.Parameters[i];
                    Type paramType = param.IsOut || param.ParameterType.IsByRef
                        ? param.ParameterType.GetElementType()
                        : param.ParameterType;
                    NSString paramName = new NSString(param.Name.FirstCharUpper());

                    object value;
                    if (dartArguments.ContainsKey(paramName))
                    {
                        NSObject argumentValue = dartArguments[paramName];

                        NSString serializedArg = (NSString)argumentValue;

                        // Deserialize the arg value
                        value = JsonConvert.DeserializeObject(serializedArg, paramType, FlutterInterop.JsonSerializerSettings);
                    }
                    else if (param.HasDefaultValue)
                    {
                        value = param.DefaultValue;
                    }
                    else
                    {
                        SendError(methodInfo, new BridgeException(BridgeErrorCode.OperationArgumentsInvalid));
                        return;
                    }

                    arguments[i] = value;
                }
            }
            catch (Exception)
            {
                SendError(methodInfo, new BridgeException(BridgeErrorCode.OperationArgumentsParsingError));
                return;
            }

            var result = BridgeRuntime.Run(operation, arguments);
            if (result.Error != null)
            {
                if (result.Error is BridgeException flutterException)
                {
                    SendError(methodInfo, flutterException);
                }
                else
                {
                    //In case of an unhandled exception, send to Flutter a verbose error message for better diagnostic
                    var error = new BridgeException(BridgeErrorCode.OperationFailed, result.Error.ToStringCleared(), result.Error);
                    SendError(methodInfo, error);
                }
            }
            else
            {
                SendResult(methodInfo, result.Result);
            }
        }

        private void OnBridgeEvent(object sender, BridgeEventArgs e)
        {
            // Prevent dispatching events to Flutter through event channel
            // if bridge is configured for WebSocket communication
            if (Mode == FlutterBridgeMode.WebSocket)
                return;

            var eventInfo = new BridgeEventInfo
            {
                InstanceId = e.ServiceName,
                EventName = e.EventName.FirstCharLower(),
                EventData = e.EventData
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

        private void SendResult(BridgeMethodInfo methodInfo, object result)
        {
            var resultValue = new Dictionary<string, object?>
            {
                { "ReturnValue", result }
            };
            var message = new BridgeMessageInfo
            {
                MethodInfo = methodInfo,
                Result = resultValue
            };

            NSObject dartReturnValue = FlutterInterop.ToMethodChannelResult(message);
            Console.WriteLine("Sending result to Flutter...");
            MainThread.BeginInvokeOnMainThread(() => _methodChannelIncoming.InvokeMethod("result", dartReturnValue));
        }

        private void SendError(BridgeMethodInfo methodInfo, BridgeException exception)
        {
            var message = new BridgeMessageInfo
            {
                MethodInfo = methodInfo,
                // NOTE: Please consider removing ErrorCode and ErrorMessage
                ErrorCode = BridgeErrorCode.OperationFailed,
                ErrorMessage = exception.Message,
                Exception = exception
            };

            NSObject dartReturnValue = FlutterInterop.ToMethodChannelResult(message);
            Console.WriteLine("Sending error to Flutter...");
            MainThread.BeginInvokeOnMainThread(() => _methodChannelIncoming.InvokeMethod("error", dartReturnValue));
        }
    }
}
