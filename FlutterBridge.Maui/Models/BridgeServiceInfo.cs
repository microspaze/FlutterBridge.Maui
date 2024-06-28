using FlutterBridge.Maui.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeServiceInfo
    {
        private readonly Type _type;
        private readonly object? _instance;
        private readonly HashSet<EventInfo> _eventSet = [];
        private readonly ConcurrentDictionary<string, Delegate> _events = new();
        private readonly ConcurrentDictionary<string, BridgeOperationInfo> _operations = new();

        public ConcurrentDictionary<string, BridgeOperationInfo> Operations => _operations;

        public BridgeServiceInfo(Type type, string serviceName, object? instance = null)
        {
            ServiceName = serviceName;
            _type = type;
            _instance = instance;

            foreach (Type typedef in type.GetBridgeServiceTypeDefinitions())
            {
                foreach (MethodInfo method in typedef.GetBridgeOperations())
                {
                    var operation = new BridgeOperationInfo(method, instance);
                    _operations.TryAdd(operation.OperationName, operation);
                }
            }
        }

        public BridgeServiceInfo(Type type, string serviceName, MethodInfo[] methodInfos, EventInfo[] eventInfos, object? instance = null)
        {
            ServiceName = serviceName;
            _type = type;
            _instance = instance;

            foreach(var methodInfo in methodInfos)
            {
                var operation = new BridgeOperationInfo(methodInfo, instance);
                _operations.TryAdd(operation.OperationName, operation);
            }

            foreach(var eventInfo in eventInfos)
            {
                _eventSet.Add(eventInfo);
            }
        }

        public string ServiceName { get; }

        public bool TryGetOperation(string operationName, out BridgeOperationInfo? operation)
        {
            return _operations.TryGetValue(operationName, out operation);
        }

        public void SubscribeToEvents()
        {
            if (_instance == null)
                return;

            // Method on class BridgeEventReceiver that will be used as handler for all the events
            MethodInfo? handleMethod = typeof(BridgeEventReceiver).GetMethod("Handle", BindingFlags.NonPublic | BindingFlags.Instance);

            var eventInfos = _eventSet.Count == 0 ? _type.GetBridgeEvents() : _eventSet.ToArray();
            foreach (EventInfo? eventInfo in eventInfos)
            {
                if (eventInfo == null) continue;

                var eventName = eventInfo.Name;
                var receiver = new BridgeEventReceiver((sender, e) =>
                {
                    BridgeRuntime.PropagateBridgeEvent(ServiceName, eventName, sender, e);
                });

                if (eventInfo.EventHandlerType != null && handleMethod != null)
                {
                    // Create an handler for the instance event
                    Delegate delegateForEvent = Delegate.CreateDelegate(eventInfo.EventHandlerType, receiver, handleMethod);

                    // Register the handler in the object instance
                    eventInfo.AddEventHandler(_instance, delegateForEvent);

                    _events.TryAdd(eventName, delegateForEvent);
                }

                #region Some documentation

                // Connect this service info with the instance event
                //Delegate delegateForEvent = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, handleMethod);
                //eventInfo.AddEventHandler(instance, delegateForEvent);

                //// Create an instance of the delegate. Using the overloads
                //// of CreateDelegate that take MethodInfo is recommended.
                //Delegate d = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, handleMethod);

                //// Get the "add" accessor of the event and invoke it late-
                //// bound, passing in the delegate instance. This is equivalent
                //// to using the += operator in C#, or AddHandler in Visual
                //// Basic. The instance on which the "add" accessor is invoked
                //// is the form; the arguments must be passed as an array.
                //MethodInfo addHandler = eventInfo.GetAddMethod();
                //Object[] addHandlerArgs = { d };
                //addHandler.Invoke(instance, addHandlerArgs);

                #endregion
            }
        }

        public void UnsubscribeFromEvents()
        {
            if (_instance == null)
                return;

            foreach (EventInfo eventInfo in _type.GetBridgeEvents())
            {
                bool exists = _events.TryRemove(eventInfo.Name, out Delegate? delegateForEvent);
                if (exists && delegateForEvent != null)
                {
                    // Unregister the handler in the object instance
                    eventInfo.RemoveEventHandler(_instance, delegateForEvent);
                }
            }
        }
    }
}
