using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    public class MusicOrder
    {        
        [JsonPropertyName("genreNo")] public int GenreNo { get; set; } = 0;
        [ReadOnly(true)]
        [JsonPropertyName("id")] public string Id { get; set; } = "ABCDEF";
        [ReadOnly(true)]
        [JsonPropertyName("uniqueId")] public int UniqueId { get; set; } = 0;
        [JsonPropertyName("closeDispType")] public int CloseDispType { get; set; } = 0;

                
        [DefaultValue(Genre.Pop)]
        [JsonIgnore] public Genre Genre 
        {
            get => (Genre)GenreNo;
            set => GenreNo = (int)value;
        }
    }
}
