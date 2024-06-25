using Android.Runtime;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlutterBridge.Maui.Attributes;
using FlutterBridge.Maui.Models;
using FlutterBridge.Maui.Extensions;

namespace FlutterBridge.Maui
{
    internal class FlutterInterop
    {
        private static readonly JsonConverter[] JsonConverters =
        {
            new StringEnumConverter(),
            new IsoDateTimeConverter
            {
                DateTimeStyles = DateTimeStyles.AdjustToUniversal
            }
        };

        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Objects,
            Converters = JsonConverters
        };

        public static JsonSerializer Serializer = JsonSerializer.Create(JsonSerializerSettings);

        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(int value)
        {
            //Java.Lang.Object? primitiveObj = JsonConvert.SerializeObject(value, JsonSerializerSettings);
            //return primitiveObj;
            return value.ToProtoBytes();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(BridgeMessageInfo message)
        {
            // FIX ISSUES ABOUT DICTIONARY IN FLUTTER
            //JObject jsonObject = JObject.FromObject(message, Serializer);
            //CleanObjectFromInvalidTypes(ref jsonObject);
            //Java.Lang.Object? obj = jsonObject.ToString(Formatting.None);
            //return obj;
            return message.ToProtoBytes();
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="Java.Lang.Object"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="FlutterBinding.Plugin.Common.MethodChannel"/> method invoke.
        /// </summary>
        public static Java.Lang.Object? ToMethodChannelResult(BridgeEventInfo message)
        {
            // FIX ISSUES ABOUT DICTIONARY IN FLUTTER
            //JObject jsonObject = JObject.FromObject(message, Serializer);
            //CleanObjectFromInvalidTypes(ref jsonObject);
            //Java.Lang.Object? obj = jsonObject.ToString(Formatting.None);
            //return obj;
            return message.ToProtoBytes();
        }

        public static void CleanObjectFromInvalidTypes(ref JObject jobject)
        {

            if (jobject.Type == JTokenType.Object && jobject.ContainsKey("$type"))
            {
                JToken? value = jobject.GetValue("$type");
                var valueStr = value?.Value<string>();
                if (!string.IsNullOrEmpty(valueStr) && valueStr.StartsWith("System")) // Remove system types like Dictionaries
                {
                    jobject.Remove("$type");
                }

                foreach (JProperty jp in jobject.Properties())
                {
                    JToken propValue = jp.Value;
                    if (propValue.Type == JTokenType.Object)
                    {
                        JObject propJObject = (JObject)propValue;
                        CleanObjectFromInvalidTypes(ref propJObject);
                    }
                }

            }

        }

        private static object? ToFlutterObject(object value)
        {
            if (value == null) return null;

            var type = value.GetType();
            var properType = Nullable.GetUnderlyingType(type) ?? type;
            if (properType.IsPrimitive || properType == typeof(string))
            {
                return value;
            }

            if (properType.IsArray && properType.IsGenericType == false)
            {
                var elementType = properType.GetElementType()!;
                var properElementType = Nullable.GetUnderlyingType(elementType) ?? elementType;
                if (properElementType == typeof(byte))
                {
                    // for performance reasons, we adopt Newtonsoft approach
                    // and convert byte[] to a base-64 encoded string
                    return (value as byte[]).ToBase64String();
                }

                var list = new JavaList();
                foreach (object item in (IList)value)
                {
                    list.Add(properElementType.IsPrimitive ? item : ToFlutterObject(item));
                }
                return list;
            }

            if (properType.IsGenericType)
            {
                Type[] interfaces = properType.IsInterface ? new[] { properType } : properType.GetInterfaces();

                bool implementsDictionary = interfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));

                if (implementsDictionary)
                {
                    Type[] types = properType.GetGenericArguments();
                    var valueType = Nullable.GetUnderlyingType(types[1]) ?? types[1];
                    var map = new JavaDictionary();
                    foreach (dynamic kvp in (IEnumerable)value)
                    {
                        map.Add(kvp.Key, valueType.IsPrimitive ? kvp.Value : ToFlutterObject(kvp.Value));
                    }
                    return map;
                }

                bool implementsEnumerable = interfaces.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                if (implementsEnumerable)
                {
                    Type[] types = properType.GetGenericArguments();
                    var itemType = Nullable.GetUnderlyingType(types[0]) ?? types[0];
                    var list = new JavaList();
                    foreach (object item in (IEnumerable)value)
                    {
                        list.Add(itemType.IsPrimitive ? item : ToFlutterObject(item));
                    }
                    return list;
                }

                throw new InvalidOperationException();
            }

            if (properType.GetCustomAttributes(typeof(BridgeDataAttribute), false).Length > 0)
            {
                return JObject.FromObject(value).ToJavaDictionary();
            }

            throw new InvalidOperationException();
        }
    }
}
