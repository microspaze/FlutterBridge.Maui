using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public sealed class BridgeDataAttribute : Attribute
    {
    }
}
