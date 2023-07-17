using System.Text.Json.Serialization;

namespace TaikoSoundEditor.Data
{
    public class MusicAttributes
    {
        [JsonPropertyName("items")]
        public List<MusicAttribute> Items { get; set; } = new List<MusicAttribute>();

        public MusicAttribute GetByUniqueId(int id)
        {
            return Items.Find(x => x.UniqueId == id);
        }

        public int GetNewId()
        {
            return Items.Max(i => i.UniqueId) + 1;
        }

        public bool IsValidSongId(string id)
        {
            return !Items.Any(i => i.Id == id);
        }
    }
}
