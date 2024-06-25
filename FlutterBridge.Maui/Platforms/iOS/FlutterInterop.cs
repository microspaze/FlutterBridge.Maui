using Foundation;
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
using FlutterBridge.Maui.Models;
using FlutterBridge.Maui.Extensions;
using FlutterBridge.Maui.Attributes;

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
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(int value)
        {
            //string json = JsonConvert.SerializeObject(value, JsonSerializerSettings);
            //return NSObject.FromObject(json);
            return NSObject.FromObject(value.ToProtoBytes());
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(BridgeMessageInfo message)
        {
            // FIX ISSUES ABOUT DICTIONARY
            //JObject jsonObject = JObject.FromObject(message, Serializer);
            //CleanObjectFromInvalidTypes(ref jsonObject);
            //return NSObject.FromObject(jsonObject.ToString(Formatting.None));
            return NSObject.FromObject(message.ToProtoBytes());
        }

        /// <summary>
        /// Converts a .NET object to a valid <see cref="NSObject"/>
        /// that can be sent to Flutter as a successful result of
        /// a <see cref="Flutnet.Interop.FlutterMethodChannel"/> method invoke.
        /// </summary>
        public static NSObject ToMethodChannelResult(BridgeEventInfo message)
        {
            // FIX ISSUES ABOUT DICTIONARY
            //JObject jsonObject = JObject.FromObject(message, Serializer);
            //CleanObjectFromInvalidTypes(ref jsonObject);
            //return NSObject.FromObject(jsonObject.ToString(Formatting.None));
            return NSObject.FromObject(message.ToProtoBytes());
        }

        public static void CleanObjectFromInvalidTypes(ref JObject jobject)
        {
            if (jobject.Type == JTokenType.Object && jobject.ContainsKey("$type"))
            {
                JToken? value = jobject.GetValue("$type");
                var typeStr = value?.Value<string>();
                if (!string.IsNullOrEmpty(typeStr) && typeStr.StartsWith("System")) // Remove system types like Dictionaries
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

        private static NSObject? ToFlutterObject(object value)
        {
            if (value == null) return null;

            var type = value.GetType();
            var properType = Nullable.GetUnderlyingType(type) ?? type;
            if (properType.IsPrimitive || properType == typeof(string))
            {
                return NSObject.FromObject(value);
            }

            if (properType.IsArray && properType.IsGenericType == false)
            {
                var elementType = properType.GetElementType()!;
                var properElementType = Nullable.GetUnderlyingType(elementType) ?? elementType;
                if (properElementType == typeof(byte))
                {
                    // for performance reasons, we adopt Newtonsoft approach
                    // and convert byte[] to a base-64 encoded string
                    return NSObject.FromObject(value.ToBase64String());
                }

                var list = new NSMutableArray();
                foreach (object item in (IList)value)
                {
                    list.Add(ToFlutterObject(item));
                }
                return list;
            }

            if (properType.IsGenericType)
            {
                bool implementsDictionary = properType
                        .GetInterfaces()
                        .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IDictionary<,>));

                //bool isDictionary = properType.GetGenericTypeDefinition() == typeof(IDictionary<,>);

                if (implementsDictionary)
                {
                    //Type[] types = properType.GetGenericArguments();
                    //Type valueType = Nullable.GetUnderlyingType(types[1]) ?? types[1];

                    var map = new NSMutableDictionary();
                    foreach (dynamic kvp in (IEnumerable)value)
                    {
                        map.Add(NSObject.FromObject(kvp.Key), ToFlutterObject(kvp.Value));
                    }
                    return map;
                }

                bool implementsEnumerable = properType
                    .GetInterfaces()
                    .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>));

                //bool isEnumerable = properType.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsEnumerable)
                {
                    //Type[] types = properType.GetGenericArguments();
                    //Type itemType = Nullable.GetUnderlyingType(types[0]) ?? types[0];

                    var list = new NSMutableArray();
                    foreach (object item in (IEnumerable)value)
                    {
                        list.Add(ToFlutterObject(item));
                    }
                    return list;
                }

                throw new InvalidOperationException();
            }

            if (properType.GetCustomAttributes(typeof(BridgeDataAttribute), false).Length > 0)
            {
                return JObject.FromObject(value).ToNSDictionary();
            }

            throw new InvalidOperationException();
        }
    }
}
