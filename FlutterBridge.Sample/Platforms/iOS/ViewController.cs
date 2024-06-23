using System;
using System.Threading.Tasks;
using FlutterBinding;
using FlutterBridge.Maui;
using FlutterBridge.Maui.Models;
using FlutterBridge.Sample.Services;
using UIKit;

namespace FlutterBridge.Sample
{
    public partial class ViewController : FlutterViewController
    {
        BridgeHost _bridge;
        bool _initialized;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            if (_initialized)
                return;

            try
            {
                BridgeRuntime.Init();
                BridgeRuntime.RegisterBridgeService(new CounterService().ToBridgeService("counter_service"));
#if DEBUG
                _bridge = new BridgeHost(this.Engine, FlutterBridgeMode.PlatformChannel);
#else
                _bridge = new BridgeHost(this.Engine, FlutterBridgeMode.PlatformChannel);
#endif
                _initialized = true;
            }
            catch (Exception e)
            {
                var tcs = new TaskCompletionSource<bool>();
                var alert = UIAlertController.Create("Fatal Error", e.Message, UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, a => tcs.SetResult(true)));
                this.PresentViewController(alert, true, null);
                await tcs.Task;
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
    }
}