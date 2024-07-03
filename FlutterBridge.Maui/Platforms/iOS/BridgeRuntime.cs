using FlutterBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    public static partial class BridgeRuntime
    {
        /// <summary>
        /// Initializes the Bridge environment.
        /// </summary>
        public static bool Init()
        {
            try
            {
                // 1.Init FlutterEngine
                var flutterEngine = new FlutterEngine(DEFAULT_ENGINE_ID);
                flutterEngine.Run();

                // 2.Init BridgeHost
                _bridge ??= new BridgeHost(flutterEngine, _bridgeMode);

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

                // 5.Set FlutterEngine
                _engine = flutterEngine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Initialized;
        }
    }
}
