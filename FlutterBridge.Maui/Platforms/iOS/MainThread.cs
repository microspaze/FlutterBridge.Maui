using Foundation;
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
        /// <summary>
        /// Returns <see langword="true"/> if code is running on the main (UI) thread.
        /// </summary>
        public static bool IsMainThread
        {
            get
            {
                return NSThread.Current.IsMainThread;
            }
        }

        /// <summary>
        /// Invokes an Action on the main (UI) thread.
        /// </summary>
        /// <param name="action">The Action to invoke</param>
        public static void BeginInvokeOnMainThread(Action action)
        {
            // The implementation of this method mimics Xamarin.Essentials MainThread
            // https://github.com/xamarin/Essentials/blob/master/Xamarin.Essentials/MainThread/MainThread.ios.tvos.watchos.cs
            // Please note: Action code is executed when the main thread goes back to its main loop for processing events.
            // For further info see: https://stackoverflow.com/a/45663945
            // and: https://docs.microsoft.com/en-US/xamarin/essentials/main-thread

            Console.WriteLine("Posting action on main thread...");
            NSRunLoop.Main.BeginInvokeOnMainThread(action.Invoke);
        }
    }
}
