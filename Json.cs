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
            return JsonSerializer.Deserialize<T>(json);
        }

        public static string Serialize<T>(T item)
        {
            return JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            });
        }

    }
}
