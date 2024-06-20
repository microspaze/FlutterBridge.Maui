using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    [Obfuscation(Exclude = true)]
    [ProtoContract]
    internal class BridgeEventInfo
    {
        [ProtoMember(1)]
        public string InstanceId { get; set; } = string.Empty;

        [ProtoMember(2)]
        public string EventName { get; set; } = string.Empty;

        [ProtoMember(3)]
        public EventArgs EventData { get; set; } = EventArgs.Empty;
    }
}
