using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    [ProtoContract]
    public enum BridgeMode
    {
        /// <summary>
        /// Communication uses standard Flutter platform channel.
        /// </summary>
        [ProtoEnum]
        PlatformChannel = 0,

        /// <summary>
        /// Communication uses WebSocket protocol.
        /// </summary>
        [ProtoEnum]
        WebSocket = 1
    }
}
