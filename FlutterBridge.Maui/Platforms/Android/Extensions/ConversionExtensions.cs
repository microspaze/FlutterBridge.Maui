using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    internal static partial class ConversionExtensions
    {
        #region JObect to Android Map

        public static Android.Runtime.JavaDictionary ToJavaDictionary(this JObject json)
        {
            var propertyValuePairs = json.ToObject<Dictionary<string, object>>();
            if (propertyValuePairs != null)
            {
                ProcessJObjectProperties2(propertyValuePairs);
                ProcessJArrayProperties2(propertyValuePairs);
                ProcessByteArrayProperties2(propertyValuePairs);
            }
            return new Android.Runtime.JavaDictionary(propertyValuePairs ?? []);
        }

        private static void ProcessJObjectProperties2(IDictionary<string, object> propertyValuePairs)
        {
            List<string> objectPropertyNames = (from property in propertyValuePairs
                                                let propertyName = property.Key
                                                let value = property.Value
                                                where value is JObject
                                                select propertyName).ToList();

            objectPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToJavaDictionary((JObject)propertyValuePairs[propertyName]));
        }

        private static void ProcessJArrayProperties2(IDictionary<string, object> propertyValuePairs)
        {
            List<string> arrayPropertyNames = (from property in propertyValuePairs
                                               let propertyName = property.Key
                                               let value = property.Value
                                               where value is JArray
                                               select propertyName).ToList();

            arrayPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToJavaList((JArray)propertyValuePairs[propertyName]));
        }

        private static void ProcessByteArrayProperties2(IDictionary<string, object> propertyValuePairs)
        {
            List<string> objectPropertyNames = (from property in propertyValuePairs
                                                let propertyName = property.Key
                                                let value = property.Value
                                                where value.GetType().IsArray && value.GetType().GetElementType() == typeof(byte)
                                                select propertyName).ToList();

            objectPropertyNames.ForEach((propertyName) =>
                propertyValuePairs[propertyName] = propertyValuePairs[propertyName].ToBase64String()
            );
        }

        public static Android.Runtime.JavaList ToJavaList(this JArray array)
        {
            var list = new Android.Runtime.JavaList();
            foreach (JToken token in array.ToList())
            {
                list.Add(ProcessArrayEntry2(token.ToObject<object>()));
            }
            return list;
        }

        private static object? ProcessArrayEntry2(object? value)
        {
            if (value == null) return null;

            if (value is JObject objValue)
            {
                return ToJavaDictionary(objValue);
            }

            if (value is JArray arrayValue)
            {
                return ToJavaList(arrayValue);
            }

            return value;
        }

        #endregion
    }
}
