using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    internal static partial class ConversionExtensions
    {
        #region JObect to .NET IDictionary

        public static IDictionary<string, object?>? ToDictionary(this JObject? json)
        {
            if (json == null) return null;

            var propertyValuePairs = json.ToObject<Dictionary<string, object?>>();
            if (propertyValuePairs != null)
            {
                ProcessJObjectProperties(propertyValuePairs);
                ProcessJArrayProperties(propertyValuePairs);
            }
            return propertyValuePairs;
        }

        private static void ProcessJObjectProperties(IDictionary<string, object?> propertyValuePairs)
        {
            List<string> objectPropertyNames = (from property in propertyValuePairs
                                                let propertyName = property.Key
                                                let value = property.Value
                                                where value is JObject
                                                select propertyName).ToList();

            objectPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToDictionary(propertyValuePairs[propertyName] as JObject));
        }

        private static void ProcessJArrayProperties(IDictionary<string, object?> propertyValuePairs)
        {
            List<string> arrayPropertyNames = (from property in propertyValuePairs
                                               let propertyName = property.Key
                                               let value = property.Value
                                               where value is JArray
                                               select propertyName).ToList();

            arrayPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToArray(propertyValuePairs[propertyName] as JArray));
        }

        public static object? ToArray(this JArray? array)
        {
            if (array == null) return null;

            if (array.Type == JTokenType.Bytes)
            {
                return JsonConvert.ToString(array);
            }

            return array.ToObject<object[]>()?.Select(ProcessArrayEntry).ToArray();
        }

        private static object? ProcessArrayEntry(object value)
        {
            if (value is JObject objValue)
            {
                return ToDictionary(objValue);
            }

            if (value is JArray arrayValue)
            {
                return ToArray(arrayValue);
            }

            return value;
        }

        #endregion

        public static string ToBase64String(this object? byteArray)
        {
            var json = JsonConvert.SerializeObject(byteArray);
            return !string.IsNullOrEmpty(json) && json.Length >= 2 ? json.Substring(1, json.Length - 2) : string.Empty;
        }
    }
}
