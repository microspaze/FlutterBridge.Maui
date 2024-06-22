using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeContextInfo
    {
        private readonly ConcurrentDictionary<string, BridgeServiceInfo> _services = new();

        public BridgeContextInfo(string name)
        {
            ContextName = name;
        }

        public string ContextName { get; }

        public bool TryAddService(BridgeServiceInfo service)
        {
            return _services.TryAdd(service.InstanceName, service);
        }

        public bool TryRemoveService(string serviceName)
        {
            return _services.TryRemove(serviceName, out _);
        }

        public bool TryGetService(string name, out BridgeServiceInfo? service)
        {
            return _services.TryGetValue(name, out service);
        }
    }
}
