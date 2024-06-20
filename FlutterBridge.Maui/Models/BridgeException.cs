using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    [Obfuscation(Exclude = true)]
    internal class BridgeException : BridgeExceptionBase
    {
        public BridgeException(BridgeErrorCode errorCode)
        {
            Code = errorCode;
        }
        public BridgeException(BridgeErrorCode errorCode, string message) : base(message)
        {
            Code = errorCode;
        }
        public BridgeException(BridgeErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            Code = errorCode;
        }

        public BridgeErrorCode Code { get; }
    }
}
