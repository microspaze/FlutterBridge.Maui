using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FlutterBridge.Maui;

namespace FlutterBridge.Sample
{
    //1.App contains MauiPage & FlutterPage together, Use FlutterView to render FlutterFragement in MauiAppCompatActivity
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
    }

    //2.App contains FlutterPage only, Use MauiFlutterActivity to launch Flutter app
    //[Activity(Label = "@string/app_name", Theme = "@style/LaunchTheme", MainLauncher = true,
    //        // FLUTTER ACTIVITY SETUP
    //        HardwareAccelerated = true,
    //        WindowSoftInputMode = SoftInput.AdjustResize,
    //        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.Keyboard |
    //                               ConfigChanges.ScreenSize | ConfigChanges.Locale |
    //                               ConfigChanges.LayoutDirection | ConfigChanges.FontScale | ConfigChanges.ScreenLayout |
    //                               ConfigChanges.Density | ConfigChanges.UiMode
    //    )
    //]
    //[MetaData("io.flutter.embedding.android.NormalTheme", Resource = "@style/AppTheme")]
    //[MetaData("io.flutter.embedding.android.SplashScreenDrawable", Resource = "@drawable/launch_background")]
    //public class MainActivity : MauiFlutterActivity
    //{
    //}
}
