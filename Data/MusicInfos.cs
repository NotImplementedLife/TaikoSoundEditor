using System.Text.Json.Serialization;

namespace TaikoSoundEditor.Data
{
    internal class MusicInfos
    {
        [JsonPropertyName("items")]
        public List<MusicInfo> Items { get; set; } = new List<MusicInfo>();
    }
}
