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
        public string? OperationKey { get; set; } = null;

        [ProtoMember(3)]
        public Dictionary<string, byte[]>? Arguments { get; set; } = null;

        [ProtoMember(4)]
        public byte[]? Result { get; set; } = null;

        [ProtoMember(5)]
        public BridgeEventInfo? EventInfo { get; set; } = null;

        [ProtoMember(6)]
        public BridgeException? Exception { get; set; } = null;

        [ProtoMember(7)]
        public BridgeErrorCode? ErrorCode { get; set; } = null;

        [ProtoMember(8)]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
