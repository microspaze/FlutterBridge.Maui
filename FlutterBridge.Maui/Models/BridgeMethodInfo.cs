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
    internal class BridgeMethodInfo
    {
        [ProtoMember(1)]
        public long RequestId { get; set; }

        [ProtoMember(2)]
        public string Instance { get; set; } = string.Empty;

        [ProtoMember(3)]
        public string Service { get; set; } = string.Empty;

        [ProtoMember(4)]
        public string Operation { get; set; } = string.Empty;
    }
}
