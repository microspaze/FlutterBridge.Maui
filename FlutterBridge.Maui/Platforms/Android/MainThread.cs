using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    [Obfuscation(Exclude = true)]
    internal static class MainThread
    {
        static volatile Handler? _handler;

        /// <summary>
        /// Returns <see langword="true"/> if code is running on the main (UI) thread.
        /// </summary>
        public static bool IsMainThread
        {
            get
            {
                if ((int)Build.VERSION.SdkInt >= (int)BuildVersionCodes.M)
                    return Looper.MainLooper.IsCurrentThread;

                return Looper.MyLooper() == Looper.MainLooper;
            }
        }

        /// <summary>
        /// Invokes an Action on the main (UI) thread.
        /// </summary>
        /// <param name="action">The Action to invoke</param>
        public static void BeginInvokeOnMainThread(Action action)
        {
            // The implementation of this method mimics Xamarin.Essentials MainThread
            // https://github.com/xamarin/Essentials/blob/master/Xamarin.Essentials/MainThread/MainThread.android.cs
            // Please note: Action code is NOT executed immediately, but is added to the Looper's message queue 
            // and is handled when the UI thread's scheduled to handle its message.
            // For further info see: https://stackoverflow.com/a/45663945
            // and: https://docs.microsoft.com/en-US/xamarin/essentials/main-thread

            if (_handler?.Looper != Looper.MainLooper && Looper.MainLooper != null)
                _handler = new Handler(Looper.MainLooper);

            Console.WriteLine("Posting action on main thread...");
            _handler?.Post(action);
        }
    }
}
