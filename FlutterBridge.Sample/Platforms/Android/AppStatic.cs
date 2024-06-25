using Android.App;
using Android.Content;
using FlutterBinding.Embedding.Engine;
using FlutterBridge.Maui;
using FlutterBridge.Sample.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Sample
{
    public static class AppStatic
    {
        static BridgeHost? _bridge;
        static ActivityLifecycleContextListener? _lifecycleListener;

        public static Context AppContext => Android.App.Application.Context;

        public static Activity? CurrentActivity => _lifecycleListener?.Activity;

        public static bool Initialized { get; private set; }

        public static void Init(Android.App.Application application)
        {
            if (Initialized)
                return;

            _lifecycleListener = new ActivityLifecycleContextListener();
            application.RegisterActivityLifecycleCallbacks(_lifecycleListener);
            ConfigureFlutnetRuntime();
        }

        public static void Init(Activity activity)
        {
            if (Initialized)
                return;

            _lifecycleListener = new ActivityLifecycleContextListener { Activity = activity };
            activity.Application?.RegisterActivityLifecycleCallbacks(_lifecycleListener);
            ConfigureFlutnetRuntime();
        }

        private static void ConfigureFlutnetRuntime()
        {
            try
            {
                BridgeRuntime.Init();
                BridgeRuntime.RegisterBridgeService(new CounterService(), "counter_service");
                Initialized = true;
            }
            catch (Exception e)
            {
                if (CurrentActivity != null)
                {
                    var builder = new AlertDialog.Builder(CurrentActivity);
                    builder.SetTitle("Fatal Error");
                    builder.SetMessage(e.Message);
                    builder.SetCancelable(false);
                    builder.SetPositiveButton("OK", (sender, args) =>
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                        Environment.Exit(0);
                    });

                    var dialog = builder.Create();
                    dialog?.Show();
                }
                else
                {
                    throw;
                }
            }
        }

        public static void ConfigureFlutnetBridge(FlutterEngine flutterEngine)
        {
            try
            {
#if DEBUG
                _bridge = new BridgeHost(flutterEngine, AppContext, FlutterBridgeMode.PlatformChannel);
#else
                _bridge = new BridgeHost(flutterEngine, AppContext);
#endif
            }
            catch (Exception e)
            {
                if (CurrentActivity != null)
                {
                    var builder = new AlertDialog.Builder(CurrentActivity);
                    builder.SetTitle("Error");
                    builder.SetMessage(e.Message);
                    builder.SetCancelable(false);
                    builder.SetPositiveButton("OK", (sender, args) => { });

                    var dialog = builder.Create();
                    dialog?.Show();
                }
                else
                {
                    throw;
                }
            }
        }

        public static void CleanUpFlutnetBridge()
        {
            _bridge?.Dispose();
            _bridge = null;
        }
    }

    class ActivityLifecycleContextListener : Java.Lang.Object, Android.App.Application.IActivityLifecycleCallbacks
    {
        readonly WeakReference<Activity?> _currentActivity = new WeakReference<Activity?>(null);

        internal Context Context => Activity ?? Android.App.Application.Context;

        internal Activity? Activity
        {
            get => _currentActivity.TryGetTarget(out var a) ? a : null;
            set => _currentActivity.SetTarget(value);
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityCreated(Activity activity, Android.OS.Bundle? savedInstanceState)
        {
            Activity = activity;
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityDestroyed(Activity activity)
        {
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityPaused(Activity activity)
        {
            Activity = activity;
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityResumed(Activity activity)
        {
            Activity = activity;
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivitySaveInstanceState(Activity activity, Android.OS.Bundle outState)
        {
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityStarted(Activity activity)
        {
        }

        void Android.App.Application.IActivityLifecycleCallbacks.OnActivityStopped(Activity activity)
        {
        }
    }
}
