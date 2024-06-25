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
    [Obfuscation(Exclude = true)]
    [ProtoContract]
    public class BridgeEventInfo
    {
        [ProtoMember(1)]
        [JsonProperty("instanceId")]
        public string InstanceId { get; set; } = string.Empty;

        [ProtoMember(2)]
        [JsonProperty("event")]
        public string EventName { get; set; } = string.Empty;

        [ProtoMember(3)]
        [JsonProperty("args")]
        public byte[]? EventData { get; set; }
    }
}
