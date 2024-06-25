using ProtoBuf;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlutterBridge.Maui.Extensions
{
    public static class ProtobufExtensions
    {
        public static byte[]? ToProtoBytes(this object? model)
        {
            byte[]? bytes = null;
            if (model != null)
            {
                try
                {
                    var bufferWriter = new ArrayBufferWriter<byte>();
                    Serializer.Serialize(bufferWriter, model);
                    bytes = bufferWriter.WrittenSpan.ToArray();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return bytes;
        }

        public static object? ToProtoObject(this byte[]? bytes, Type type)
        {
            object? result = null;
            if (bytes != null && bytes.Length > 0)
            {
                try
                {
                    using var stream = new MemoryStream(bytes);
                    result = Serializer.Deserialize(type, stream);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return result;
        }

        public static T? ToProtoModel<T>(this byte[]? bytes)
        {
            var result = default(T);
            if (bytes != null && bytes.Length > 0)
            {
                try
                {
                    using var stream = new MemoryStream(bytes);
                    result = Serializer.Deserialize<T>(stream);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return result;
        }
    }
}
