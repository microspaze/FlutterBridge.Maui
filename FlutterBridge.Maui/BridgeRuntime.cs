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
    /// Runtime where you can store your BridgeService.
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

        private static readonly BridgeContextInfo _context = new(string.Empty);

        /// <summary>
        /// Creates a named class registration of a bridge service instance.
        /// </summary>
        /// <param name="instance">Instance to register</param>
        /// <param name="name">Name of registration</param>
        public static void RegisterBridgeService(object instance, string? name = null)
        {
            ArgumentNullException.ThrowIfNull(instance);

            EnsureInitialized();

            Type type = instance.GetType();
            if (!type.IsValidBridgeService())
                throw new ArgumentException("Instance does not represent a valid bridge service.", nameof(instance));

            if (string.IsNullOrEmpty(name))
            {
                name = type.Name.FirstCharLower();
            }

            var service = new BridgeServiceInfo(type, name, instance);
            if (!_context.TryAddService(service))
                throw new ArgumentException("A service has already been registered with the same name.", nameof(service.ServiceName));

            service.SubscribeToEvents();
        }

        /// <summary>
        /// Creates a named class registration of a static bridge service.
        /// </summary>
        /// <param name="type">Type to register</param>
        public static void RegisterStaticBridgeService(Type type, string? name = null)
        {
            EnsureInitialized();

            // This is a sufficient check since an abstract class cannot be sealed or static in C#
            if (!(type.IsAbstract && type.IsSealed))
                throw new ArgumentException("The provided type is not a static class.", nameof(type));

            object[] attributes = type.GetCustomAttributes(typeof(BridgeServiceAttribute), false);
            if (attributes.Length == 0)
                throw new ArgumentException("Service class must be decorated with PlatformService attribute.", nameof(type));

            if (string.IsNullOrEmpty(name))
            {
                name = type.Name.FirstCharLower();
            }

            var service = new BridgeServiceInfo(type, name);
            if (!_context.TryAddService(service))
                throw new ArgumentException("A service has already been registered with the same name.", nameof(name));
        }

        /// <summary>
        /// Removes a named class registration of a bridge service.
        /// </summary>
        /// <param name="name">Name of registration</param>
        /// <returns>true if the registration is successfully found and removed; otherwise, false.</returns>
        public static bool UnregisterBridgeService(string name)
        {
            EnsureInitialized();

            if (string.IsNullOrEmpty(name))
                return false;

            bool serviceExists = _context.TryGetService(name, out BridgeServiceInfo? service);
            if (serviceExists)
            {
                service?.UnsubscribeFromEvents();
            }

            return _context.TryRemoveService(name);
        }

        internal static BridgeOperationInfo? GetOperation(string operationKey)
        {
            BridgeOperationInfo? operationObj = null;
            try
            {
                if (!_context.TryGetOperation(operationKey, out operationObj))
                    throw new ArgumentException($"Operation not found by key {operationKey}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

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
        internal static void PropagateBridgeEvent(string serviceName, string eventName, object sender, object? eventArgs)
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