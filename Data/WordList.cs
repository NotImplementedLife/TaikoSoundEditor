using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    public class WordList
    {
        [JsonPropertyName("items")]
        public List<Word> Items { get; set; } = new List<Word>();

        public Word GetBySong(string song) => Items.Find(i => i.Key == $"song_{song}");
        public Word GetBySongSub(string song) => Items.Find(i => i.Key == $"song_sub_{song}");
        public Word GetBySongDetail(string song) => Items.Find(i => i.Key == $"song_detail_{song}");        
    }
}
