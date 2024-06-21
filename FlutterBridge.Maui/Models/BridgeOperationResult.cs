using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeOperationResult
    {
        public object? Result { get; set; }
        public Exception? Error { get; set; }
    }
}
