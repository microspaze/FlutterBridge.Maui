using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeEventArgs : EventArgs
    {
        public string ServiceName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public EventArgs EventData { get; set; } = Empty;
    }
}
