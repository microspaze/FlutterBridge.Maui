using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui
{
    internal class BridgeOperationRunner
    {
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
    }

    internal class BridgeOperationResult
    {
        public object? Result { get; set; }
        public Exception? Error { get; set; }
    }
}
