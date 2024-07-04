using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    [ProtoContract]
    public class BridgeException : Exception
    {
        public BridgeException()
        {
        }

        public BridgeException(BridgeErrorCode errorCode)
        {
            Code = errorCode;
        }
        public BridgeException(BridgeErrorCode errorCode, string message) : base(message)
        {
            Code = errorCode;
            Message = message;
        }
        public BridgeException(BridgeErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            Code = errorCode;
            Message = message;
        }

        public BridgeException(string message) : base(message)
        {
            Message = message;
        }

        public BridgeException(string message, Exception inner) : base(message, inner)
        {
            Message = message;
        }

        [ProtoMember(1)]
        public BridgeErrorCode Code { get; set; } = BridgeErrorCode.OperationFailed;

        [ProtoMember(2)]
        public override string Message { get; } = string.Empty;
    }
}
