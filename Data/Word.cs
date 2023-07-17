using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    public class Word
    {
        [ReadOnly(true)]
        [JsonPropertyName("key")] public string Key { get; set; } = "song_...";
        [JsonPropertyName("japaneseText")] public string JapaneseText { get; set; } = "text...";
        [JsonPropertyName("japaneseFontType")] public int JapaneseFontType { get; set; } = 0;       
    }
}
