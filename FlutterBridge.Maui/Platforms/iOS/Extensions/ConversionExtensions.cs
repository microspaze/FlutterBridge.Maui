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
        
        public static NSData ToByteData(this byte[]? dataBytes)
        {
            if (dataBytes == null)
            {
                return null;
            }

            return NSData.FromArray(dataBytes);
        }
        
        public static object? ToObject(this NSObject data, Type dataType)
        {
            object? value = null;
            try
            {
                if (data is NSNumber num)
                {
                    #region NSNumber Value

                    if (num is NSDecimalNumber d)
                    {
                        // handle NSDecimalNumber type directly
                        value = (decimal)d.NSDecimalValue;
                    }
                    else
                    {
                        value = num.ObjCType switch
                        {
                            "c" when num.Class.Name == "__NSCFBoolean" => num.BoolValue, // ObjC bool
                            "c" => num.SByteValue, // signed char
                            "i" => num.Int32Value, // signed int
                            "s" => num.Int16Value, // signed short
                            "l" => num.Int32Value, // signed long (32 bit)
                            "q" => num.Int64Value, // signed long long (64 bit)
                            "C" => num.ByteValue, // unsigned char
                            "I" => num.UInt32Value, // unsigned int
                            "S" => num.UInt16Value, // unsigned short
                            "L" => num.UInt32Value, // unsigned long (32 bit)
                            "Q" => num.UInt64Value, // unsigned long long (64 bit)
                            "f" => num.FloatValue, // float
                            "d" => num.DoubleValue, // double
                            "B" => num.BoolValue, // C++ bool or C99 _Bool
                            _ => throw new ArgumentOutOfRangeException(nameof(num), num,
                                $"NSNumber \"{num.StringValue}\" has an unknown ObjCType \"{num.ObjCType}\" (Class: \"{num.Class.Name}\")")
                        };
                    }

                    #endregion
                }
                else if (data is NSString str)
                {
                    #region NSString Value

                    value = str.ToString();

                    #endregion
                }
                else
                {
                    value = Convert.ChangeType(data, dataType);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return value;
        }
    }
}
