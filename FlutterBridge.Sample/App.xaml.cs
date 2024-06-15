using Android.App;
using Android.Content;

namespace FlutterBridge.Sample
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }

    public static class AppStatic
    {
        static ActivityLifecycleContextListener? _lifecycleListener;

        public static Context AppContext => Android.App.Application.Context;

        public static Activity CurrentActivity => _lifecycleListener?.Activity;

        public static bool Initialized { get; private set; }

        public static void Init(Android.App.Application application)
        {
            if (Initialized)
                return;

            _lifecycleListener = new ActivityLifecycleContextListener();
            application.RegisterActivityLifecycleCallbacks(_lifecycleListener);
            Initialized = true;
        }

        public static void Init(Activity activity)
        {
            if (Initialized)
                return;

            _lifecycleListener = new ActivityLifecycleContextListener { Activity = activity };
            activity.Application.RegisterActivityLifecycleCallbacks(_lifecycleListener);
            Initialized = true;
        }
    }

    class ActivityLifecycleContextListener : Java.Lang.Object, Android.App.Application.IActivityLifecycleCallbacks
    {
        readonly WeakReference<Activity> _currentActivity = new WeakReference<Activity>(null);

        internal Context Context => Activity ?? Android.App.Application.Context;

        internal Activity Activity
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
