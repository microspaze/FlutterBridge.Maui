using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
    public class BridgeMessageInfo
    {
        [ProtoMember(1)]
        [JsonProperty("methodInfo")]
        public BridgeMethodInfo? MethodInfo { get; set; }

        [ProtoMember(2)]
        [JsonProperty("arguments")]
        public Dictionary<string, object>? Arguments { get; set; }

        [ProtoMember(3)]
        [JsonProperty("result")]
        public Dictionary<string, object?>? Result { get; set; }

        [ProtoMember(4)]
        [JsonProperty("event")]
        public BridgeEventInfo? EventInfo { get; set; }

        [ProtoMember(5)]
        [JsonProperty("exception")]
        public BridgeExceptionBase? Exception { get; set; }

        [ProtoMember(6)]
        [JsonProperty("errorCode")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BridgeErrorCode? ErrorCode { get; set; }

        [ProtoMember(7)]
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
