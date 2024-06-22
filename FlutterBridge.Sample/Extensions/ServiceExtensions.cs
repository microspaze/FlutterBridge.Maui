using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Sample.Extensions
{
    public static class ServiceExtensions
    {
        public static BridgeServiceInfo ToBridgeService(this object service, string instanceName)
        {
            var serviceType = service.GetType();
            var methodInfos = serviceType.GetBridgeMethodInfos();
            var eventInfos = serviceType.GetBridgeEventInfos();
            return new BridgeServiceInfo(serviceType, instanceName, methodInfos, eventInfos, service);
        }

        private static MethodInfo[] GetBridgeMethodInfos(this Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0)
                return new MethodInfo[0];

            List<MethodInfo> methods = new List<MethodInfo>();

            if (type.IsInterface)
            {
                methods.AddRange(type.GetMethods()
                        .Where(m => m.GetCustomAttribute(typeof(BridgeOperationAttribute), true) != null));

                foreach (Type inherited in type.GetInterfaces())
                    methods.AddRange(inherited.GetBridgeMethodInfos());
            }
            else
            {
                // right now explicit interface methods are NOT supported
                methods.AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Where(m => m.GetCustomAttribute(typeof(BridgeOperationAttribute), true) != null));
            }

            return methods.ToArray();
        }

        private static EventInfo[] GetBridgeEventInfos(this Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0)
                return new EventInfo[0];

            return type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(e => e.GetCustomAttribute(typeof(BridgeEventAttribute), false) != null)
                .Where(e => e.IsSupportedBridgeEvent())
                .ToArray();
        }

        private static bool IsSupportedBridgeEvent(this EventInfo e)
        {
            return e.EventHandlerType == typeof(EventHandler) ||
                   e.EventHandlerType.IsGenericType && e.EventHandlerType.GetGenericTypeDefinition() == typeof(EventHandler<>);
        }
    }
}
