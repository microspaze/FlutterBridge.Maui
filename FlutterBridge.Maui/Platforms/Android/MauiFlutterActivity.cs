using FlutterBinding.Embedding.Android;
using FlutterBinding.Embedding.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    public class MauiFlutterActivity : FlutterActivity
    {
        public override void ConfigureFlutterEngine(FlutterEngine flutterEngine)
        {
            base.ConfigureFlutterEngine(flutterEngine);
            BridgeRuntime.Init(this, flutterEngine);
        }
    }
}
