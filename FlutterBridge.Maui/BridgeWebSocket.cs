using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace FlutterBridge.Maui
{
    internal class BridgeWebSocket
    {
        readonly WebSocketServer _server;

        public BridgeWebSocket()
        {
            _server = new WebSocketServer(12345)
            {
                WaitTime = TimeSpan.MaxValue
            };
            _server.AddWebSocketService<BridgeWebSocketBehavior>("/flutter");
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }
    }

    internal class BridgeWebSocketBehavior : WebSocketBehavior
    {
        readonly object _responseBufferLock = new();
        readonly Queue<BridgeMessageInfo> _responseBufferQueue = new(20);

        public BridgeWebSocketBehavior()
        {
            BridgeRuntime.OnBridgeEvent += BridgeRuntimeOnBridgeEvent;
        }

        private void BridgeRuntimeOnBridgeEvent(object? sender, BridgeEventArgs e)
        {
            BridgeEventInfo eventInfo = new BridgeEventInfo
            {
                InstanceId = e.ServiceName,
                EventName = e.EventName.FirstCharLower(),
                EventData = e.EventData.ToProtoBytes(),
            };

            BridgeMessageInfo message = new BridgeMessageInfo()
            {
                MethodInfo = new BridgeMethodInfo
                {
                    RequestId = -1,
                    Instance = e.ServiceName
                },
                Result = null,
                EventInfo = eventInfo
            };

            Send(message);
        }

        private void AddToBuffer(BridgeMessageInfo message)
        {
            lock (_responseBufferLock)
            {
                BridgeMessageInfo? found = _responseBufferQueue.FirstOrDefault(r => r.MethodInfo != null && r.MethodInfo.RequestId == message.MethodInfo?.RequestId);
                if (found == null)
                {
                    _responseBufferQueue.Enqueue(message);

                    if (_responseBufferQueue.Count > 20)
                    {
                        _responseBufferQueue.Dequeue();
                    }
                }
            }
        }

        private BridgeMessageInfo? GetFromBuffer(int requestId)
        {
            lock (_responseBufferLock)
            {
                BridgeMessageInfo? response = _responseBufferQueue.FirstOrDefault(r => r.MethodInfo != null && r.MethodInfo.RequestId == requestId);
                return response;
            }
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.RawData == null)
                return;

            if (e.RawData != null)
            {
                BridgeMessageInfo? request;
                try
                {
                    request = e.RawData.ToProtoModel<BridgeMessageInfo>();
                }
                catch (Exception)
                {
                    // ignore invalid messages
                    return;
                }

                if (request == null || request.MethodInfo == null)
                {
                    return;
                }

                BridgeOperationInfo operation;
                try
                {
                    operation = BridgeRuntime.GetOperation(request.MethodInfo.Instance, request.MethodInfo.Operation);
                }
                catch (Exception)
                {
                    BridgeException error = new BridgeException(BridgeErrorCode.OperationNotImplemented);
                    SendError(request.MethodInfo, error);
                    return;
                }

                var requestArgsCount = request.Arguments?.Count ?? 0;
                var operationParamsCount = operation.Parameters?.Length ?? 0;
                if (operationParamsCount != requestArgsCount)
                {
                    BridgeException error = new BridgeException(BridgeErrorCode.OperationArgumentsCountMismatch);
                    SendError(request.MethodInfo, error);
                    return;
                }

                var arguments = new object?[operationParamsCount];
                try
                {
                    for (int i = 0; i < operationParamsCount; i++)
                    {
                        ParameterInfo param = operation.Parameters![i];
                        Type paramType = param.IsOut || param.ParameterType.IsByRef
                            ? param.ParameterType.GetElementType()!
                            : param.ParameterType;
                        string paramName = param.Name!.FirstCharUpper();

                        object? value;
                        if (request.Arguments!.ContainsKey(paramName))
                        {
                            byte[]? argumentValue = request.Arguments[paramName];
                            if (argumentValue == null)
                            {
                                value = null;
                            }
                            else if (argumentValue.GetType() == paramType)
                            {
                                value = argumentValue;
                            }
                            else
                            {
                                value = argumentValue.ToProtoObject(paramType);
                            }
                        }
                        else if (param.HasDefaultValue)
                        {
                            value = param.DefaultValue;
                        }
                        else
                        {
                            BridgeException error = new BridgeException(BridgeErrorCode.OperationArgumentsInvalid);
                            SendError(request.MethodInfo, error);
                            return;
                        }

                        arguments[i] = value;
                    }
                }
                catch (Exception)
                {
                    BridgeException error = new BridgeException(BridgeErrorCode.OperationArgumentsParsingError);
                    SendError(request.MethodInfo, error);
                    return;
                }

                var result = BridgeRuntime.Run(operation, arguments);
                if (result.Error != null)
                {
                    if (result.Error is BridgeException flutterException)
                    {
                        SendError(request.MethodInfo, flutterException);
                    }
                    else
                    {
                        //In case of an unhandled exception, send to Flutter a verbose error message for better diagnostic
                        BridgeException error = new BridgeException(BridgeErrorCode.OperationFailed, result.Error.ToStringCleared(), result.Error);
                        SendError(request.MethodInfo, error);
                    }
                }
                else
                {
                    SendResult(request.MethodInfo, result.Result);
                }
            }
        }

        private void SendError(BridgeMethodInfo methodInfo, BridgeException exception)
        {
            var message = new BridgeMessageInfo
            {
                MethodInfo = methodInfo,
                // NOTE: Please consider remove ErrorCode and ErrorMessage
                ErrorCode = BridgeErrorCode.OperationFailed,
                ErrorMessage = exception.Message,
                Exception = exception
            };

            Send(message);
        }

        private void SendResult(BridgeMethodInfo methodInfo, object? result)
        {
            var message = new BridgeMessageInfo
            {
                MethodInfo = methodInfo,
                Result = result.ToProtoBytes(),
            };

            Send(message);
        }

        private void Send(BridgeMessageInfo message)
        {
            try
            {
                Send(message.ToProtoBytes());
            }
            catch (Exception)
            {
                AddToBuffer(message);
            }
        }
    }
}
