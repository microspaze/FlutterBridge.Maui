using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Helpers
{
    /// <summary>
    /// On iOS platform the type extensions method will not return the correct reflection info, 
    /// but returns only reflection info of Object, so all those extensions should change to helper methods.
    /// </summary>
    internal static class TypeHelper
    {
        #region System.Type global extensions

        /// <summary>
        /// Gets a valute indicating whether this type is a nullable type.
        /// </summary>
        /// <returns>True if nullable, otherwise False</returns>
        public static bool IsNullable(Type type, out Type? underlyingType)
        {
            underlyingType = Nullable.GetUnderlyingType(type);
            return underlyingType != null;
        }

        /// <summary>
        /// Gets a valute indicating whether this type is a generic type.
        /// </summary>
        /// <returns>True if generic, otherwise False</returns>
        public static bool IsGeneric(Type type)
        {
            return type.IsGenericType
                   && type.Name.Contains('`'); //TODO: Figure out why IsGenericType isn't good enough and document (or remove) this condition
        }

        /// <summary>
        /// Gets the fully qualified type name of this type.
        /// This will use any keywords in place of types where possible (string instead of System.String for example).
        /// </summary>
        /// <param name="type"></param>
        /// <returns>The fully qualified name for this type</returns>
        public static string GetQualifiedTypeName(Type type)
        {
            switch (type.Name)
            {
                case nameof(Char):
                    return "char";
                case nameof(String):
                    return "string";
                case nameof(Int16):
                    return "short";
                case nameof(Int32):
                    return "int";
                case nameof(Int64):
                    return "long";
                case nameof(UInt16):
                    return "ushort";
                case nameof(UInt32):
                    return "uint";
                case nameof(UInt64):
                    return "ulong";
                case nameof(Single):
                    return "float";
                case nameof(Double):
                    return "double";
                case nameof(Decimal):
                    return "decimal";
                case nameof(Object):
                    return "object";
                case nameof(Boolean):
                    return "bool";
                case nameof(Byte):
                    return "byte";
                case nameof(SByte):
                    return "sbyte";
                case "Void":
                    return "void";
            }

            //TODO: Figure out how type.FullName could be null and document (or remove) this condition
            string signature = string.IsNullOrWhiteSpace(type.FullName)
                ? type.Name
                : type.FullName;

            if (IsGeneric(type))
                signature = RemoveGenericTypeNameArgumentCount(signature);

            return signature;
        }

        /// <summary>
        /// Removes the `{argument-count} from the signature of a generic type.
        /// </summary>
        /// <param name="genericTypeSignature">Signature of a generic type</param>
        /// <returns><paramref name="genericTypeSignature"/> without any argument count</returns>
        private static string RemoveGenericTypeNameArgumentCount(string genericTypeSignature)
        {
            return genericTypeSignature.Substring(0, genericTypeSignature.IndexOf('`'));
        }

        #endregion

        #region Flutter support utilities

        static readonly Type[] FlutterUnsupportedPrimitiveTypes =
        {
            typeof(ushort), typeof(uint), typeof(ulong), typeof(nint), typeof(nuint)
        };

        static readonly Type[] FlutterSupportedBuiltinTypes =
        {
            typeof(string), typeof(object)
        };

        public static bool IsFlutterSupportedType(Type type)
        {
            Type t = Nullable.GetUnderlyingType(type) ?? type;

            if (t.IsPrimitive)
            {
                return !FlutterUnsupportedPrimitiveTypes.Contains(t);
            }

            if (t.IsArray)
            {
                Type elementType = t.GetElementType();
                return IsFlutterSupportedType(elementType);
            }

            if (t.IsGenericType)
            {
                bool implementsDictionary = t.GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));

                //bool isDictionary = t.GetGenericTypeDefinition() == typeof(IDictionary<,>);

                if (implementsDictionary)
                {
                    Type[] types = t.GetGenericArguments();
                    return types[0] == typeof(string) && IsFlutterSupportedType(types[1]);
                }

                bool implementsEnumerable = t.GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                //bool isEnumerable = t.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsEnumerable)
                {
                    Type[] types = t.GetGenericArguments();
                    return IsFlutterSupportedType(types[0]);
                }

                return false;
            }

            return t.GetCustomAttributes(typeof(BridgeDataAttribute), false).Length > 0 || FlutterSupportedBuiltinTypes.Contains(t);
        }

        #endregion

        #region BridgeService + BridgeOperation utilities

        public static bool IsValidBridgeService(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(BridgeServiceAttribute), true);
            if (attributes.Length == 0)
            {
                if (type.IsInterface)
                {
                    // This interface isn't decorated with BridgeService attribute
                    // nor any of the inherited interfaces
                    return false;
                }

                // This class isn't decorated with BridgeService attribute
                // nor any of its base classes
                // Try searching on implemented interfaces...
                return type.GetInterfaces()
                    .Any(i => i.GetCustomAttribute(typeof(BridgeServiceAttribute), true) != null);
            }
            return true;
        }

        public static Type[] GetBridgeServiceTypeDefinitions(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(BridgeServiceAttribute), true);
            if (attributes.Length == 0)
            {
                if (type.IsInterface)
                {
                    // This interface isn't decorated with BridgeService attribute
                    // nor any of the inherited interfaces
                    return new Type[0];
                }

                // This class isn't decorated with BridgeService attribute
                // nor any of its base classes
                // Try searching on implemented interfaces...
                return type.GetInterfaces()
                    .Where(i => i.GetCustomAttribute(typeof(BridgeServiceAttribute), true) != null)
                    .ToArray();
            }
            return new[] { type };
        }

        public static MethodInfo[] GetBridgeOperations(Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0)
                return new MethodInfo[0];

            List<MethodInfo> methods = new List<MethodInfo>();

            if (type.IsInterface)
            {
                methods.AddRange(type.GetMethods()
                        .Where(m => m.GetCustomAttribute(typeof(BridgeOperationAttribute), true) != null));

                foreach (Type inherited in type.GetInterfaces())
                    methods.AddRange(GetBridgeOperations(inherited));
            }
            else
            {
                // right now explicit interface methods are NOT supported
                methods.AddRange(type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                    .Where(m => m.GetCustomAttribute(typeof(BridgeOperationAttribute), true) != null));
            }

            return methods.ToArray();
        }

        public static MethodInfo[] GetUnsupportedBridgeOperations(Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0 || type.IsInterface)
                return new MethodInfo[0];

            // right now private methods (including explicit interface methods) are NOT supported
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(m => m.GetCustomAttribute(typeof(BridgeOperationAttribute), true) != null)
                .ToArray();
        }

        #endregion

        #region BridgeEvent utilities

        public static EventInfo[] GetBridgeEvents(Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0)
                return new EventInfo[0];

            return type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(e => e.GetCustomAttribute(typeof(BridgeEventAttribute), false) != null)
                .Where(e => IsSupportedBridgeEvent(e))
                .ToArray();
        }

        public static EventInfo[] GetUnsupportedBridgeEvents(Type type)
        {
            if (type.GetCustomAttributes(typeof(BridgeServiceAttribute), true).Length == 0)
                return new EventInfo[0];

            return type.GetEvents(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                .Where(e => e.GetCustomAttribute(typeof(BridgeEventAttribute), false) != null)
                .Where(e => !IsSupportedBridgeEvent(e))
                .ToArray();
        }

        private static bool IsSupportedBridgeEvent(EventInfo e)
        {
            return e.EventHandlerType == typeof(EventHandler) ||
                   e.EventHandlerType.IsGenericType && e.EventHandlerType.GetGenericTypeDefinition() == typeof(EventHandler<>);
        }

        public static Type GetBridgeEventArgs(EventInfo e)
        {
            if (e.EventHandlerType.IsGenericType && e.EventHandlerType.GetGenericTypeDefinition() == typeof(EventHandler<>))
            {
                return e.EventHandlerType.GenericTypeArguments.First();
            }
            else if (e.EventHandlerType == typeof(EventHandler))
            {
                return typeof(EventArgs);
            }
            else
            {
                throw new InvalidOperationException("Cannot call this method when the target event is not a supported BridgeEvent.");
            }

        }

        #endregion

        #region BridgeException utilities

        public static bool IsValidBridgeException(Type type)
        {
            return typeof(BridgeExceptionBase).IsAssignableFrom(type);
        }

        #endregion

        #region System.Threading.Tasks.Task global extensions

        public static bool IsTask(Type type)
        {
            return type == typeof(Task) || IsGeneric(type) && type.GetGenericTypeDefinition() == typeof(Task<>);
        }

        public static bool IsTaskVoid(Type type)
        {
            return type == typeof(Task);
        }

        public static bool IsTaskT(Type type)
        {
            return IsGeneric(type) && type.GetGenericTypeDefinition() == typeof(Task<>);
        }

        public static Type GetResultType(Task task)
        {
            return task.GetType().GetProperty("Result")?.PropertyType ?? typeof(void);
        }

        public static object GetResultValue(Task task)
        {
            return task.GetType().GetProperty("Result")?.GetValue(task);
        }

        public static object TaskResult(Task task)
        {
            if (GetResultType(task) == typeof(void))
            {
                return null;
            }
            else
            {
                return GetResultValue(task);
            }
        }

        #endregion
    }
}
