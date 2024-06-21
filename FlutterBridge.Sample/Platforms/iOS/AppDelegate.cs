using Foundation;
using UIKit;
using FlutterBinding;

namespace FlutterBridge.Sample
{
    //[Register("AppDelegate")]
    //public class AppDelegate : MauiUIApplicationDelegate
    //{
    //    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    //}

    [Register("AppDelegate")]
    public class AppDelegate : FlutterAppDelegate
    {
        private readonly FlutterEngine _flutterEngine = new("flutterbridge");

        [Export("application:didFinishLaunchingWithOptions:")]
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            _flutterEngine.Run();
            return base.FinishedLaunching(application, launchOptions);
        }
    }
}
