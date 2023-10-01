using System.Text.Json.Serialization;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Data
{
    public interface IMusicOrder
    {
        int GenreNo { get; set; }
        string Id { get; set; }
        int UniqueId { get; set; }
        int CloseDispType { get; set; }

        [JsonIgnore]
        [Recast("GenreNo")]
        Genre Genre { get; set; }        
    }
}
