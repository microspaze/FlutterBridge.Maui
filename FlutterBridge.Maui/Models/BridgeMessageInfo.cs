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
    internal class BridgeMessageInfo
    {
        [ProtoMember(1)]
        public BridgeMethodInfo? MethodInfo { get; set; }

        [ProtoMember(2)]
        public Dictionary<string, object>? Arguments { get; set; }

        [ProtoMember(3)]
        public Dictionary<string, object?>? Result { get; set; }

        [ProtoMember(4)]
        public BridgeEventInfo? EventInfo { get; set; }

        [ProtoMember(5)]
        public BridgeExceptionBase? Exception { get; set; }

        [ProtoMember(6)]
        public BridgeErrorCode? ErrorCode { get; set; }

        [ProtoMember(7)]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
