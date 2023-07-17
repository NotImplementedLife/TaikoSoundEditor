using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    public class MusicOrders
    {
        [JsonPropertyName("items")]
        public List<MusicOrder> Items { get; set; } = new List<MusicOrder>();

        public MusicOrder GetByUniqueId(int id) => Items.Find(i => i.UniqueId == id);
    }
}
