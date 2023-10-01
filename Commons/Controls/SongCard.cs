using TaikoSoundEditor.Data;
using TaikoSoundEditor.Commons.Extensions;
using TaikoSoundEditor.Commons.Utils;
using System.Drawing;
using TaikoSoundEditor.Collections;

namespace TaikoSoundEditor.Commons.Controls
{
    public class SongCard
    {
        internal WordList WordList { get; }
        public IMusicOrder MusicOrder { get; }

        public string Id => $"{MusicOrder.UniqueId}.{MusicOrder.Id}";
        public string Title => WordList.GetBySong(MusicOrder.Id).JapaneseText;
        public string Subtitle => WordList.GetBySongSub(MusicOrder.Id).JapaneseText;
        public string Genre => MusicOrder.Genre.ToString();
        public Color Color => Constants.GenreColors[MusicOrder.GenreNo.Clamp(0, Constants.GenreColors.Length - 1)];

        public SongCard(WordList wordList, IMusicOrder musicOrder)
        {
            WordList = wordList;
            MusicOrder = musicOrder;
        }

        public bool IsSelected { get; set; } = false;

        public bool IsCut { get; set; } = false;
    }
}
