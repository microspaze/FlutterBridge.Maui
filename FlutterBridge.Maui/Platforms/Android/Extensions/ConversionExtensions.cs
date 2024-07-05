using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = Java.Lang.Object;

namespace FlutterBridge.Maui.Extensions
{
    internal static partial class ConversionExtensions
    {
        private const string _boolTypeName = "Boolean";
        private const string _intTypeName = "Integer";
        private const string _longTypeName = "Long";
        private const string _floatTypeName = "Float";
        private const string _doubleTypeName = "Double";
        private const string _stringTypeName = "String";
        private const string _bytesTypeName = "byte[]";
        private const string _intsTypeName = "int[]";
        private const string _longsTypeName = "long[]";
        private const string _floatsTypeName = "float[]";
        private const string _doublesTypeName = "double[]";

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

        public static object? ToObject(this Object? data, Type dataType)
        {
            object? value = null;
            if (data == null) return value;
            try
            {
                var simpleTypeName = data.Class.SimpleName;
                switch (simpleTypeName)
                {
                    case _boolTypeName:
                        value = (bool?)data;
                        break;
                    case _intTypeName:
                        value = (int?)data;
                        break;
                    case _longTypeName:
                        value = (long?)data;
                        break;
                    case _floatTypeName:
                        value = (float?)data;
                        break;
                    case _doubleTypeName:
                        value = (double?)data;
                        break;
                    case _stringTypeName:
                        value = (string?)data;
                        break;
                    case _bytesTypeName:
                        var dataBytes = (byte[]?)data;
                        value = dataBytes.ToProtoObject(dataType);
                        break;
                    case _intsTypeName:
                        value = (int[]?)data;
                        break;
                    case _longsTypeName:
                        value = (long[]?)data;
                        break;
                    case _floatsTypeName:
                        value = (float[]?)data;
                        break;
                    case _doublesTypeName:
                        value = (double[]?)data;
                        break;
                    default:
                        value = Convert.ChangeType(data, dataType);
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return value;
        }
    }
}
