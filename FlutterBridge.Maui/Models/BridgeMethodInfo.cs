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
    public class BridgeMethodInfo
    {
        [ProtoMember(1)]
        [JsonProperty("requestId")]
        public long RequestId { get; set; }

        [ProtoMember(2)]
        [JsonProperty("instance")]
        public string Instance { get; set; } = string.Empty;

        [ProtoMember(3)]
        [JsonProperty("service")]
        public string Service { get; set; } = string.Empty;

        [ProtoMember(4)]
        [JsonProperty("operation")]
        public string Operation { get; set; } = string.Empty;
    }
}
