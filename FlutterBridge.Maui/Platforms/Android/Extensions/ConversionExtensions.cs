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
