using Foundation;
using UIKit;
using FlutterBinding;

namespace FlutterBridge.Sample
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

    //[Register("AppDelegate")]
    //public class AppDelegate : FlutterAppDelegate
    //{
    //    [Export("application:didFinishLaunchingWithOptions:")]
    //    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    //    {
    //        return base.FinishedLaunching(application, launchOptions);
    //    }
    //}
}
