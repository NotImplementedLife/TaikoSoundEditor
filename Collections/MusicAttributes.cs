using System.Linq;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor.Collections
{
    public class MusicAttributes : Collection<IMusicAttribute>
    {
        public IMusicAttribute GetByUniqueId(int id)
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
