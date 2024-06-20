using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeExceptionBase : Exception
    {
        public BridgeExceptionBase()
        {
        }

        public BridgeExceptionBase(string message) : base(message)
        {
            Message = message;
        }

        public BridgeExceptionBase(string message, Exception inner) : base(message, inner)
        {
            Message = message;
        }

        public override string Message { get; } = string.Empty;
    }
}
