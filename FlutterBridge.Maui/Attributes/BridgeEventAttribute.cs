using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Attributes
{
    [AttributeUsage(AttributeTargets.Event)]
    public sealed class BridgeEventAttribute : Attribute
    {
    }
}
