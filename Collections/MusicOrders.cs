using TaikoSoundEditor.Data;

namespace TaikoSoundEditor.Collections
{
    internal class MusicOrders : Collection<IMusicOrder>
    {
        public IMusicOrder GetByUniqueId(int id) => Items.Find(i => i.UniqueId == id);
    }
}
