using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    internal static class SignatureExtensions
    {
        /// <summary>
        /// Get a fully qualified signature for <paramref name="type"/>
        /// </summary>
        /// <param name="type">Type. May be generic or <see cref="Nullable{T}"/></param>
        /// <returns>Fully qualified signature</returns>
        public static string GetSignature(this Type type)
        {
            var isNullableType = type.IsNullable(out Type underlyingNullableType);
            var signatureType = isNullableType ? underlyingNullableType : type;
            var signature = signatureType.GetQualifiedTypeName();
            var isGenericType = signatureType.IsGeneric();
            if (isGenericType)
            {
                // Add the generic arguments
                signature += BuildGenericSignature(signatureType.GetGenericArguments());
            }

            if (isNullableType)
            {
                signature += "?";
            }

            return signature ?? string.Empty;
        }

        public static string GetSignature(this MethodInfo method, bool invokable)
        {
            StringBuilder sb = new();

            // Add our method accessors if it's not invokable
            if (!invokable)
            {
                sb.Append(method.GetMethodAccessorSignature());
                sb.Append(' ');
            }

            // Add method name
            sb.Append(method.Name);

            // Add method generics
            if (method.IsGenericMethod)
            {
                sb.Append(method.GetTypeParametersSignature());
            }

            // Add method parameters
            sb.Append(method.GetMethodArgumentsSignature(invokable));

            return sb.ToString();
        }

        /// <summary>
        /// Returns the signature of the specified method according to C# language specification.
        /// The signature of a method consists of the name of the method, the number of type parameters and the type and kind
        /// (value, reference, or output) of each of its formal parameters, considered in the order left to right. [...]
        /// The signature of a method specifically does not include the return type,
        /// the params modifier that may be specified for the right-most parameter,
        /// nor the optional type parameter constraints.
        /// For further reading see: https://stackoverflow.com/a/33712878
        /// </summary>
        public static string GetCSharpSignature(this MethodInfo method)
        {
            StringBuilder sb = new();

            // Add method name
            sb.Append(method.Name);

            // Add type parameters if it's a generic method (i.e. <string, string>)
            if (method.IsGenericMethod)
            {
                sb.Append(method.GetTypeParametersSignature());
            }

            // Add formal parameters (i.e. (int, string, DateTime))
            sb.Append(method.GetFormalParametersSignature());

            return sb.ToString();
        }

        public static string GetMethodAccessorSignature(this MethodInfo method)
        {
            StringBuilder sb = new();

            // Access level
            if (method.IsPublic)
            {
                sb.Append("public ");
            }
            else if (method.IsPrivate)
            {
                sb.Append("private ");
            }
            else if (method.IsFamily)
            {
                sb.Append("protected ");
            }
            else if (method.IsAssembly)
            {
                sb.Append("internal ");
            }
            else if (method.IsFamilyOrAssembly)
            {
                sb.Append("protected internal ");
            }

            // Member or static
            if (method.IsStatic)
                sb.Append("static ");

            // Abstract or virtual
            if (method.IsAbstract)
            {
                sb.Append("abstract ");
            }
            else if (method.IsVirtual)
            {
                sb.Append("virtual ");
            }

            // Return type
            sb.Append(method.ReturnType.GetSignature());

            return sb.ToString();
        }

        public static string GetMethodArgumentsSignature(this MethodInfo method, bool invokable)
        {
            var isExtensionMethod = method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false);
            var methodParameters = method.GetParameters().AsEnumerable();

            // If this signature is designed to be invoked and it's an extension method
            if (isExtensionMethod && invokable)
            {
                // Skip the first argument
                methodParameters = methodParameters.Skip(1);
            }

            var methodParameterSignatures = methodParameters.Select(param =>
            {
                var signature = string.Empty;

                if (param.ParameterType.IsByRef)
                    signature = "ref ";
                else if (param.IsOut)
                    signature = "out ";
                else if (isExtensionMethod && param.Position == 0)
                    signature = "this ";

                if (!invokable)
                {
                    signature += param.ParameterType.GetSignature() + " ";
                }

                signature += param.Name;

                return signature;
            });

            var methodParameterString = "(" + string.Join(", ", methodParameterSignatures) + ")";

            return methodParameterString;
        }

        public static string GetTypeParametersSignature(this MethodInfo method)
        {
            ArgumentNullException.ThrowIfNull(method);

            if (!method.IsGenericMethod)
                throw new ArgumentException($"{method.Name} is not generic.");

            return BuildGenericSignature(method.GetGenericArguments());
        }

        public static string GetFormalParametersSignature(this MethodInfo method)
        {
            List<string> signatures = [];
            foreach (ParameterInfo parameterInfo in method.GetParameters())
            {
                Type? parameterType = parameterInfo.IsOut || parameterInfo.ParameterType.IsByRef
                    ? parameterInfo.ParameterType.GetElementType()
                    : parameterInfo.ParameterType;
                if (parameterType == null) { continue; }

                if (parameterInfo.IsOut)
                {
                    signatures.Add("out " + parameterType.GetSignature());
                }
                else if (parameterInfo.ParameterType.IsByRef)
                {
                    signatures.Add("ref " + parameterType.GetSignature());
                }
                else
                {
                    signatures.Add(parameterType.GetSignature());
                }
            }

            string signature = "(" + string.Join(", ", signatures) + ")";
            return signature;
        }

        /// <summary>
        /// Takes an <see cref="IEnumerable{T}"/> and creates a generic type signature (&lt;string, string&gt; for example)
        /// </summary>
        /// <param name="genericArgumentTypes"></param>
        /// <returns>Generic type signature like &lt;Type, ...&gt;</returns>
        private static string BuildGenericSignature(IEnumerable<Type> genericArgumentTypes)
        {
            IEnumerable<string> argumentSignatures = genericArgumentTypes.Select(GetSignature);

            return "<" + string.Join(", ", argumentSignatures) + ">";
        }
    }
}
