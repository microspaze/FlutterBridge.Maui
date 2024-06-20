using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Helpers;
using FlutterBridge.Maui.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    /// <summary>
    /// Runtime where you can store your PlatformService.
    /// </summary>
    public static class BridgeRuntime
    {
        #region Initialization

        internal const string EnvironmentNotInitializedMsg = "The Bridge environment has not been initialized yet. Please call BridgeRuntime.Init().";

        /// <summary>
        /// Initializes the Bridge environment.
        /// </summary>
        public static void Init()
        {
            Initialized = true;
        }

        internal static bool Initialized { get; private set; }

        internal static void EnsureInitialized()
        {
            if (!Initialized)
                throw new BridgeException(BridgeErrorCode.EnvironmentNotInitialized, EnvironmentNotInitializedMsg);
        }

        #endregion

        #region Registry

        static readonly ConcurrentDictionary<string, ContextInfo> _contexts = new ConcurrentDictionary<string, ContextInfo>();

        internal class ContextInfo
        {
            readonly ConcurrentDictionary<string, ServiceInfo> _services = new ConcurrentDictionary<string, ServiceInfo>();

            public ContextInfo(string name)
            {
                ContextName = name;
            }

            public string ContextName { get; }

            public bool TryAddService(ServiceInfo service)
            {
                return _services.TryAdd(service.ServiceInstanceName, service);
            }

            public bool TryRemoveService(string serviceName)
            {
                return _services.TryRemove(serviceName, out _);
            }

            public bool TryGetService(string name, out ServiceInfo service)
            {
                return _services.TryGetValue(name, out service);
            }
        }

        internal class ServiceEventReceiver
        {
            readonly Action<object, EventArgs> _handle;

            public ServiceEventReceiver(Action<object, EventArgs> handle)
            {
                _handle = handle;
            }

            [Obfuscation(Exclude = true)]
            private void Handle(object sender, EventArgs args)
            {
                _handle.Invoke(sender, args);
            }
        }

        internal class ServiceInfo
        {
            private readonly Type _type;
            private readonly object _instance;

            readonly ConcurrentDictionary<string, BridgeOperationInfo> _operations = new ConcurrentDictionary<string, BridgeOperationInfo>();

            public ServiceInfo(Type type, string instanceName, object instance = null)
            {
                ServiceInstanceName = instanceName;
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

            public string ServiceInstanceName { get; }

            public bool TryGetOperation(string name, out BridgeOperationInfo operation)
            {
                return _operations.TryGetValue(name, out operation);
            }

            readonly ConcurrentDictionary<string, Delegate> _events = new ConcurrentDictionary<string, Delegate>();

            public void SubscribeToEvents()
            {
                if (_instance == null)
                    return;

                // Method on class ServiceEventReceiver that will be used as handler for all the events
                MethodInfo? handleMethod = typeof(ServiceEventReceiver).GetMethod("Handle", BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (EventInfo? eventInfo in _type.GetBridgeEvents())
                {
                    var eventName = eventInfo.Name;
                    var receiver = new ServiceEventReceiver((sender, e) =>
                    {
                        PropagateBridgeEvent(ServiceInstanceName, eventName, sender, e);
                    });

                    // Create an handler for the instance event
                    Delegate delegateForEvent = Delegate.CreateDelegate(eventInfo.EventHandlerType, receiver, handleMethod);

                    // Register the handler in the object instance
                    eventInfo.AddEventHandler(_instance, delegateForEvent);

                    _events.TryAdd(eventName, delegateForEvent);

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
                    bool exists = _events.TryRemove(eventInfo.Name, out Delegate delegateForEvent);
                    if (exists && delegateForEvent != null)
                    {
                        // Unregister the handler in the object instance
                        eventInfo.RemoveEventHandler(_instance, delegateForEvent);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a named class registration of a platform service instance.
        /// </summary>
        /// <param name="instance">Instance to register</param>
        /// <param name="name">Name of registration</param>
        public static void RegisterPlatformService(object instance, string name)
        {
            EnsureInitialized();

            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            Type type = instance.GetType();
            if (!type.IsValidBridgeService())
                throw new ArgumentException("Instance does not represent a valid platform service.", nameof(instance));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Registration name cannot be null or empty.", nameof(name));

            // Right now use the default context
            string contextName = string.Empty;
            ContextInfo contextObj = _contexts.GetOrAdd(contextName, s => new ContextInfo(s));

            ServiceInfo service = new ServiceInfo(type, name, instance);
            if (!contextObj.TryAddService(service))
                throw new ArgumentException("A service has already been registered with the same name.", nameof(name));

            service.SubscribeToEvents();
        }

        /// <summary>
        /// Creates a named class registration of a static platform service.
        /// </summary>
        /// <param name="type">Type to register</param>
        /// <param name="name">Name of registration</param>
        public static void RegisterStaticPlatformService(Type type, string name)
        {
            EnsureInitialized();

            // This is a sufficient check since an abstract class cannot be sealed or static in C#
            if (!(type.IsAbstract && type.IsSealed))
                throw new ArgumentException("The provided type is not a static class.", nameof(type));

            object[] attributes = type.GetCustomAttributes(typeof(BridgeServiceAttribute), false);
            if (attributes.Length == 0)
                throw new ArgumentException("Service class must be decorated with PlatformService attribute.", nameof(type));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Registration name cannot be null or empty.", nameof(name));

            // Right now use the default context
            string contextName = string.Empty;
            ContextInfo contextObj = _contexts.GetOrAdd(contextName, s => new ContextInfo(s));

            ServiceInfo service = new ServiceInfo(type, name);
            if (!contextObj.TryAddService(service))
                throw new ArgumentException("A service has already been registered with the same name.", nameof(name));
        }

        /// <summary>
        /// Removes a named class registration of a platform service.
        /// </summary>
        /// <param name="name">Name of registration</param>
        /// <returns>true if the registration is successfully found and removed; otherwise, false.</returns>
        public static bool UnregisterPlatformService(string name)
        {
            EnsureInitialized();

            if (string.IsNullOrEmpty(name))
                return false;

            // Right now use the default context
            string contextName = string.Empty;
            if (!_contexts.TryGetValue(contextName, out ContextInfo? contextObj))
                return false;

            bool serviceExists = contextObj.TryGetService(name, out ServiceInfo service);

            if (serviceExists)
            {
                service?.UnsubscribeFromEvents();
            }

            return contextObj.TryRemoveService(name);
        }

        internal static BridgeOperationInfo GetOperation(string serviceName, string operation)
        {
            // Right now use the default context
            string contextName = string.Empty;
            if (!_contexts.TryGetValue(contextName, out ContextInfo? contextObj) ||
                !contextObj.TryGetService(serviceName, out ServiceInfo serviceObj))
                throw new ArgumentException("No service registered with the specified name.", nameof(serviceName));

            if (!serviceObj.TryGetOperation(operation, out BridgeOperationInfo operationObj))
                throw new ArgumentException("Operation not found on the specified service.", nameof(operation));

            return operationObj;
        }

        /// <summary>
        /// Occurs when a .NET event that must be propagated to Flutter is raised.
        /// </summary>
        internal static event EventHandler<BridgeEventArgs>? OnBridgeEvent;

        /// <summary>
        /// Called when a .NET event that must be propagated to Flutter is raised.
        /// This method propagates the event through <see cref="OnBridgeEvent"/>
        /// so that <see cref="FlutterBridge"/> can subscribe and send data to Flutter.
        /// </summary>
        private static void PropagateBridgeEvent(string serviceName, string eventName, object sender, EventArgs eventArgs)
        {
            var args = new BridgeEventArgs
            {
                ServiceName = serviceName,
                EventName = eventName,
                EventData = eventArgs
            };

            OnBridgeEvent?.Invoke(sender, args);
        }

        #endregion
    }

    internal class BridgeEventArgs : EventArgs
    {
        public string ServiceName { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public EventArgs EventData { get; set; } = EventArgs.Empty;
    }
}
