using System.Text.Json;

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
            return JsonSerializer.Serialize(item, new JsonSerializerOptions { WriteIndented = true });
        }

    }
}
