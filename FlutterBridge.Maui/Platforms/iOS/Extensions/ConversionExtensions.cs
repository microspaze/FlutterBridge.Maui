using Newtonsoft.Json.Linq;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    internal static partial class ConversionExtensions
    {
        #region JObect to iOS NSDictionary

        public static NSDictionary ToNSDictionary(this JObject json)
        {
            var nsdict = new NSMutableDictionary();
            var propertyValuePairs = json.ToObject<Dictionary<string, object>>();
            if (propertyValuePairs != null)
            {
                ProcessJObjectProperties2(propertyValuePairs);
                ProcessJArrayProperties2(propertyValuePairs);

                foreach (KeyValuePair<string, object> kvp in propertyValuePairs)
                {
                    nsdict.Add(new NSString(kvp.Key), NSObject.FromObject(kvp.Value));
                }
            }

            return nsdict;
        }

        private static void ProcessJObjectProperties2(IDictionary<string, object> propertyValuePairs)
        {
            List<string> objectPropertyNames = (from property in propertyValuePairs
                                                let propertyName = property.Key
                                                let value = property.Value
                                                where value is JObject
                                                select propertyName).ToList();

            objectPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToNSDictionary((JObject)propertyValuePairs[propertyName]));
        }

        private static void ProcessJArrayProperties2(IDictionary<string, object> propertyValuePairs)
        {
            List<string> arrayPropertyNames = (from property in propertyValuePairs
                                               let propertyName = property.Key
                                               let value = property.Value
                                               where value is JArray
                                               select propertyName).ToList();

            arrayPropertyNames.ForEach(propertyName =>
                propertyValuePairs[propertyName] = ToNSArray((JArray)propertyValuePairs[propertyName]));
        }

        public static NSArray ToNSArray(this JArray array)
        {
            var nsarray = new NSMutableArray();
            foreach (JToken token in array)
            {
                nsarray.Add(ProcessArrayEntry2(token.ToObject<object>()));
            }
            return nsarray;
        }

        private static NSObject ProcessArrayEntry2(object? value)
        {
            if (value is JObject objValue)
            {
                return ToNSDictionary(objValue);
            }

            if (value is JArray arrayValue)
            {
                return ToNSArray(arrayValue);
            }

            return NSObject.FromObject(value);
        }

        #endregion

        public static byte[] ToByteArray(this NSData data)
        {
            var dataBytes = new byte[data.Length];
            System.Runtime.InteropServices.Marshal.Copy(data.Bytes, dataBytes, 0, Convert.ToInt32(data.Length));
            return dataBytes;
        }
    }
}
