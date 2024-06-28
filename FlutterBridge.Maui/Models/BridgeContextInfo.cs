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
        private readonly ConcurrentDictionary<string, BridgeOperationInfo> _operations = new();

        public BridgeContextInfo(string name)
        {
            ContextName = name;
        }

        public string ContextName { get; }

        public bool TryAddService(BridgeServiceInfo service)
        {
            var serviceName = service.ServiceName;
            var added = _services.TryAdd(serviceName, service);
            if (added)
            {
                foreach(var operation in service.Operations)
                {
                    var operationKey = $"{serviceName}.{operation.Key}";
                    _operations.TryAdd(operationKey, operation.Value);
                }
            }

            return added;
        }

        public bool TryRemoveService(string serviceName)
        {
            var removed = _services.TryRemove(serviceName, out var serviceInfo);
            if (removed && serviceInfo != null)
            {
                foreach (var operation in serviceInfo.Operations)
                {
                    var operationKey = $"{serviceName}.{operation.Key}";
                    _operations.TryRemove(operationKey, out _);
                }
            }
            return removed;
        }

        public bool TryGetService(string serviceName, out BridgeServiceInfo? service)
        {
            return _services.TryGetValue(serviceName, out service);
        }

        public bool TryGetOperation(string operationKey, out BridgeOperationInfo? operation)
        {
            return _operations.TryGetValue(operationKey, out operation);
        }
    }
}
