using FlutterBridge.Maui.Extensions;
using ProtoBuf;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeEventReceiver
    {
        readonly Action<object, byte[]?> _handle;

        public BridgeEventReceiver(Action<object, byte[]?> handle)
        {
            _handle = handle;
        }

        [Obfuscation(Exclude = true)]
        private void Handle(object sender, object? args)
        {
            _handle.Invoke(sender, args.ToProtoBytes());
        }
    }
}
