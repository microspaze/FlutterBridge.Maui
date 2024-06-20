using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    public enum FlutterBridgeMode
    {
        /// <summary>
        /// Communication uses standard Flutter platform channel.
        /// </summary>
        PlatformChannel,
        /// <summary>
        /// Communication uses WebSocket protocol.
        /// </summary>
        WebSocket
    }
}
