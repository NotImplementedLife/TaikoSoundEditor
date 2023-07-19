using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace TaikoSoundEditor
{
    internal static class Json
    {
        public static T Deserialize<T>(string json)
        {
            Logger.Info($"Deserializing {typeof(T)} ({json.Length})");
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string Serialize<T>(T item)
        {
            Logger.Info($"Serializing {typeof(T)}:\n{item}");
            return JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });
        }

    }
}
