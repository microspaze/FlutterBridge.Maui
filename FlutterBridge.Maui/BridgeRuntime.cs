using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Extensions;
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

        static readonly ConcurrentDictionary<string, BridgeContextInfo> _contexts = new();

        /// <summary>
        /// Creates a named class registration of a platform service instance.
        /// </summary>
        /// <param name="service"></param>
        public static void RegisterBridgeService(BridgeServiceInfo service)
        {
            EnsureInitialized();

            // Right now use the default context
            string contextName = string.Empty;
            BridgeContextInfo contextObj = _contexts.GetOrAdd(contextName, s => new BridgeContextInfo(s));

            if (!contextObj.TryAddService(service))
                throw new ArgumentException("A service has already been registered with the same name.", nameof(service.InstanceName));

            service.SubscribeToEvents();
        }

        /// <summary>
        /// Creates a named class registration of a platform service instance.
        /// </summary>
        /// <param name="instance">Instance to register</param>
        /// <param name="name">Name of registration</param>
        public static void RegisterBridgeService(object instance, string name)
        {
            if (instance == null)
                throw new ArgumentNullException(nameof(instance));

            Type type = instance.GetType();
            if (!type.IsValidBridgeService())
                throw new ArgumentException("Instance does not represent a valid platform service.", nameof(instance));

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Registration name cannot be null or empty.", nameof(name));

            RegisterBridgeService(new BridgeServiceInfo(type, name, instance));
        }

        /// <summary>
        /// Creates a named class registration of a static platform service.
        /// </summary>
        /// <param name="type">Type to register</param>
        /// <param name="name">Name of registration</param>
        public static void RegisterStaticBridgeService(Type type, string name)
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
            BridgeContextInfo contextObj = _contexts.GetOrAdd(contextName, s => new BridgeContextInfo(s));

            BridgeServiceInfo service = new BridgeServiceInfo(type, name);
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
            if (!_contexts.TryGetValue(contextName, out BridgeContextInfo? contextObj))
                return false;

            bool serviceExists = contextObj.TryGetService(name, out BridgeServiceInfo service);

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
            if (!_contexts.TryGetValue(contextName, out BridgeContextInfo? contextObj) ||
                !contextObj.TryGetService(serviceName, out BridgeServiceInfo serviceObj))
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
        /// so that <see cref="BridgeHost"/> can subscribe and send data to Flutter.
        /// </summary>
        internal static void PropagateBridgeEvent(string serviceName, string eventName, object sender, EventArgs eventArgs)
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

        #region Runner

        /// <summary>
        /// Invoke a platform operation with the specified arguments.
        /// </summary>
        public static BridgeOperationResult Run(BridgeOperationInfo operation, object?[] arguments)
        {
            // Check if the operation must be invoked on the main (UI) thread

            object? operationResult = null;
            Exception? operationError = null;

            // 1. Async call on UI Thread
            var mainThreadRequired = operation.OperationAttribute?.MainThreadRequired == true;
            if (mainThreadRequired && operation.IsAsyncTask)
            {
                ManualResetEvent uiFinishEvent = new ManualResetEvent(false);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var task = operation.DelegateWithResult?.Invoke(arguments) as Task;
                    task?.ContinueWith(t =>
                    {
                        if (t.IsFaulted)
                        {
                            operationError = t.Exception?.GetBaseException();
                        }
                        else if (t.IsCanceled)
                        {
                            operationError = new BridgeException(BridgeErrorCode.OperationCanceled);
                        }
                        else
                        {
                            operationResult = t.TaskResult();
                        }
                        uiFinishEvent.Set();
                    });
                });
                uiFinishEvent.WaitOne();
            }
            // 2. Sync call on UI Thread
            else if (mainThreadRequired)
            {
                var uiFinishEvent = new ManualResetEvent(false);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        if (operation.HasResult)
                        {
                            operationResult = operation.DelegateWithResult?.Invoke(arguments);
                        }
                        else
                        {
                            operation.Delegate?.Invoke(arguments);
                            operationResult = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        operationError = ex;
                    }
                    finally
                    {
                        uiFinishEvent.Set();
                    }
                });
                uiFinishEvent.WaitOne();
            }
            // 3. Async call on Background Thread
            else if (operation.IsAsyncTask)
            {
                var taskFinishEvent = new ManualResetEvent(false);
                var task = operation.DelegateWithResult?.Invoke(arguments) as Task;
                task?.ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        operationError = t.Exception?.GetBaseException();
                    }
                    else if (t.IsCanceled)
                    {
                        operationError = new BridgeException(BridgeErrorCode.OperationCanceled);
                    }
                    else
                    {
                        operationResult = t.TaskResult();
                    }
                    taskFinishEvent.Set();
                });
                taskFinishEvent.WaitOne();
            }
            // 4. Sync call on Background Thread
            else
            {
                try
                {
                    if (operation.HasResult)
                    {
                        operationResult = operation.DelegateWithResult?.Invoke(arguments);
                    }
                    else
                    {
                        operation.Delegate?.Invoke(arguments);
                        operationResult = null;
                    }
                }
                catch (Exception ex)
                {
                    operationError = ex;
                }
            }

            // Return the result
            return new BridgeOperationResult
            {
                Result = operationResult,
                Error = operationError
            };
        }

        #endregion
    }
}