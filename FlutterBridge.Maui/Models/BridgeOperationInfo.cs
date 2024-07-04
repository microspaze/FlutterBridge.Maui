using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Models
{
    public class BridgeOperationInfo
    {
        public BridgeOperationInfo(MethodInfo method, object? serviceInstance = null)
        {
            MethodName = method.Name;
            HasResult = method.ReturnType.Name != "Void";
            Parameters = method.GetParameters();
            OperationAttribute = method.GetCustomAttribute(typeof(BridgeOperationAttribute), true) as BridgeOperationAttribute;
            OperationName = OperationAttribute != null && !string.IsNullOrEmpty(OperationAttribute.Name) ? OperationAttribute.Name : method.GetCSharpSignature(); 
            IsAsyncTask = method.ReturnType.IsTask();

            if (method.IsStatic)
            {
                if (HasResult)
                    DelegateWithResult = ExpressionHelper.CreateLazyStaticMethodWithResult(method);
                else
                    Delegate = ExpressionHelper.CreateLazyStaticMethodWithNoResult(method);
            }
            else
            {
                if (HasResult)
                    DelegateWithResult = ExpressionHelper.CreateLazyMethodWithResult(serviceInstance, method);
                else
                    Delegate = ExpressionHelper.CreateLazyMethodWithNoResult(serviceInstance, method);
            }
        }

        public string OperationName { get; } = string.Empty;

        public string MethodName { get; } = string.Empty;

        public bool HasResult { get; }

        public bool IsAsyncTask { get; }

        public BridgeOperationAttribute? OperationAttribute { get; }

        public Action<object?[]>? Delegate { get; }

        public Func<object?[], object>? DelegateWithResult { get; }

        public ParameterInfo[]? Parameters { get; }

        public int ParametersCount => Parameters == null ? 0 : Parameters.Length;
    }
}
