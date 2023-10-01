using System.Linq;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.IO
{
    internal static class Json
    {
        public static object Deserialize(Type type, string json)
        {
            var method = typeof(Json).GetMethods().Where(_ => _.Name == "Deserialize" && _.GetParameters().Length == 1)
                .First().MakeGenericMethod(type);
            return method.Invoke(null, new object[] { json });
        }


        public static T Deserialize<T>(string json)
        {
            Logger.Info($"Deserializing {typeof(T)} ({json.Length})");
            return JsonSerializer.Deserialize<T>(json);
        }


        public static string Serialize<T>(T item, bool indented = true)
        {
            Logger.Info($"Serializing {typeof(T)}:\n{item}");
            return JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = indented
            });
        }

        public static string DynamicSerialize(object item, bool indented = true)
        {
            Logger.Info($"Serializing dynamic {item.GetType()}:\n{item}");

            return JsonSerializer.Serialize(item, item.GetType(), new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = indented
            });
        }

    }
}
