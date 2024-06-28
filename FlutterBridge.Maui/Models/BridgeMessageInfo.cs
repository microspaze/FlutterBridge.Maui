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
    [ProtoContract]
    public class BridgeMessageInfo
    {
        [ProtoMember(1)]
        public long RequestId { get; set; }

        [ProtoMember(2)]
        public string? OperationKey { get; set; }

        [ProtoMember(3)]
        public Dictionary<string, byte[]>? Arguments { get; set; }

        [ProtoMember(4)]
        public byte[]? Result { get; set; }

        [ProtoMember(5)]
        public BridgeEventInfo? EventInfo { get; set; }

        [ProtoMember(6)]
        public BridgeException? Exception { get; set; }

        [ProtoMember(7)]
        public BridgeErrorCode? ErrorCode { get; set; }

        [ProtoMember(8)]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
