using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Models;
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
            var eventInfo = new BridgeEventInfo
            {
                ServiceName = e.ServiceName,
                EventName = e.EventName.FirstCharLower(),
                EventData = e.EventData.ToProtoBytes(),
            };

            var message = new BridgeMessageInfo()
            {
                RequestId = -1,
                OperationKey = $"{e.ServiceName}.{e.EventName}",
                Result = null,
                EventInfo = eventInfo
            };

            Send(message);
        }

        private void AddToBuffer(BridgeMessageInfo message)
        {
            lock (_responseBufferLock)
            {
                BridgeMessageInfo? found = _responseBufferQueue.FirstOrDefault(r => r.RequestId == message.RequestId);
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

        private BridgeMessageInfo? GetFromBuffer(long requestId)
        {
            lock (_responseBufferLock)
            {
                BridgeMessageInfo? response = _responseBufferQueue.FirstOrDefault(r => r.RequestId == requestId);
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
                long requestId = 0;
                string? operationKey = null;
                try
                {
                    request = e.RawData.ToProtoModel<BridgeMessageInfo>();
                    if (request != null)
                    {
                        requestId = request.RequestId;
                        operationKey = request.OperationKey;
                    }
                }
                catch (Exception)
                {
                    // ignore invalid messages
                    return;
                }

                if (request == null || string.IsNullOrEmpty(operationKey))
                {
                    return;
                }

                var operation = BridgeRuntime.GetOperation(operationKey);
                if (operation == null)
                {
                    var error = new BridgeException(BridgeErrorCode.OperationNotImplemented);
                    SendError(requestId, operationKey, error);
                    return;
                }

                var requestArgsCount = request.Arguments?.Count ?? 0;
                var operationParamsCount = operation.Parameters?.Length ?? 0;
                if (operationParamsCount != requestArgsCount)
                {
                    var error = new BridgeException(BridgeErrorCode.OperationArgumentsCountMismatch);
                    SendError(requestId, operationKey, error);
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
                        string paramName = param.Name!;

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
                            var error = new BridgeException(BridgeErrorCode.OperationArgumentsInvalid);
                            SendError(requestId, operationKey, error);
                            return;
                        }

                        arguments[i] = value;
                    }
                }
                catch (Exception)
                {
                    var error = new BridgeException(BridgeErrorCode.OperationArgumentsParsingError);
                    SendError(requestId, operationKey, error);
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
                        BridgeException error = new BridgeException(BridgeErrorCode.OperationFailed, result.Error.ToStringCleared(), result.Error);
                        SendError(requestId, operationKey, error);
                    }
                }
                else
                {
                    SendResult(requestId, operationKey, result.Result);
                }
            }
        }

        private void SendError(long requestId, string operationKey, BridgeException exception)
        {
            var message = new BridgeMessageInfo
            {
                RequestId = requestId,
                OperationKey = operationKey,
                ErrorCode = exception.Code,
                ErrorMessage = exception.Message,
                Exception = exception
            };

            Send(message);
        }

        private void SendResult(long requestId, string operationKey, object? result)
        {
            var message = new BridgeMessageInfo
            {
                RequestId = requestId,
                OperationKey = operationKey,
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
