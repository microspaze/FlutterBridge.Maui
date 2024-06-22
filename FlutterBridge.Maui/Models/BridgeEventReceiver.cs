using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeEventReceiver
    {
        readonly Action<object, EventArgs> _handle;

        public BridgeEventReceiver(Action<object, EventArgs> handle)
        {
            _handle = handle;
        }

        [Obfuscation(Exclude = true)]
        private void Handle(object sender, EventArgs args)
        {
            _handle.Invoke(sender, args);
        }
    }
}
