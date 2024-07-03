using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    public static class AppBuilderExtensions
    {
        public static MauiAppBuilder UseFlutterBridge(this MauiAppBuilder builder, Action<FlutterConfig>? configUpdate = null)
        {
            //Update config
            configUpdate?.Invoke(FlutterConfig.Instance);

            builder
                .ConfigureLifecycleEvents(lifecycle =>
                {
#if ANDROID
                    lifecycle.AddAndroid(b =>
                    {
                        b.OnCreate((activity, state) =>
                        {
                            if (activity is not MauiFlutterActivity)
                            {
                                BridgeRuntime.Init(activity);
                            }
                        });
                    });
#elif IOS
                    lifecycle.AddiOS(b =>
                    {
                        b.FinishedLaunching((application, launchOptions) => BridgeRuntime.Init());
                    });
#endif
                })
                .ConfigureMauiHandlers(handlers =>
            {
#if ANDROID
                handlers.AddHandler(typeof(FlutterView), typeof(FlutterViewHandler));
#elif IOS
                handlers.AddHandler(typeof(FlutterView), typeof(FlutterViewHandler));
#elif MACCATALYST
                
#elif WINDOWS
                
#endif
            });

            return builder;
        }
    }
}
