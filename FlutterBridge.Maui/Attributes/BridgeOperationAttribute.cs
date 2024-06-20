using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class BridgeOperationAttribute : Attribute
    {
        /// <summary>
        /// Indicates whether the method must be invoked on the main (UI) thread
        /// when the underlying platform is iOS.
        /// </summary>
        public bool MainThreadRequired { get; set; }
    }
}
