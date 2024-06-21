using FlutterBridge.Maui.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Sample.Services
{
    [BridgeData]
    public class ValueChangedEventArgs : EventArgs
    {
        public int Value { get; set; }
    }
}
