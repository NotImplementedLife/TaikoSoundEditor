using TaikoSoundEditor.Data;

namespace TaikoSoundEditor.Collections
{
    public class WordList : Collection<IWord>
    {                
        public IWord GetBySong(string song) => Items.Find(i => i.Key == $"song_{song}");
        public IWord GetBySongSub(string song) => Items.Find(i => i.Key == $"song_sub_{song}");
        public IWord GetBySongDetail(string song) => Items.Find(i => i.Key == $"song_detail_{song}");        
    }    
}
