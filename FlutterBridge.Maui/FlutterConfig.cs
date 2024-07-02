using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    public class FlutterConfig
    {
        public static FlutterConfig Instance { get; } = new();

        public string InitialRoute { get; set; } = string.Empty;

        public bool EnableWebsocket = false;

        public Dictionary<string, object>? BridgeServices { get; set; }
    }
}
