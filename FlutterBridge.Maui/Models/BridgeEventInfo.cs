using Newtonsoft.Json;
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
    public class BridgeEventInfo
    {
        [ProtoMember(1)]
        public string ServiceName { get; set; } = string.Empty;

        [ProtoMember(2)]
        public string EventName { get; set; } = string.Empty;

        [ProtoMember(3)]
        public byte[]? EventData { get; set; }
    }
}
