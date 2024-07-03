using AndroidX.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using FlutterBinding.Embedding.Engine;
using static FlutterBinding.Embedding.Engine.Dart.DartExecutor;
using FlutterBinding.Embedding.Android;

namespace FlutterBridge.Maui
{
    public static partial class BridgeRuntime
    {
        /// <summary>
        /// Initializes the Bridge environment.
        /// </summary>
        public static void Init(Android.App.Activity activity, FlutterEngine? flutterEngine = null)
        {
            try
            {
                // 1.Init FlutterEngine
                if (flutterEngine == null)
                {
                    flutterEngine = new FlutterEngine(activity);
                    var initialRoute = _config.InitialRoute;
                    if (!string.IsNullOrEmpty(initialRoute))
                    {
                        flutterEngine.NavigationChannel.SetInitialRoute(initialRoute);
                    }
                    flutterEngine.DartExecutor.ExecuteDartEntrypoint(DartEntrypoint.CreateDefault());
                    FlutterEngineCache.Instance.Put(DEFAULT_ENGINE_ID, flutterEngine);
                }

                // 2.Init BridgeHost
                _bridge ??= new BridgeHost(flutterEngine, activity, _config.EnableWebsocket ? FlutterBridgeMode.WebSocket : FlutterBridgeMode.PlatformChannel);

                // 3.Set Initialized
                Initialized = true;

                // 4.Init BridgeService
                var services = _config.BridgeServices;
                if (services != null && services.Count > 0)
                {
                    foreach (var serviceName in services.Keys)
                    {
                        var service = services[serviceName];
                        if (service != null)
                        {
                            RegisterBridgeService(service, serviceName);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
