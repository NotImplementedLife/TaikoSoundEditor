using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor
{
    partial class MainForm
    {
        private string MusicAttributePath { get; set; }
        private string MusicOrderPath { get; set; }
        private string MusicInfoPath { get; set; }
        private string WordListPath { get; set; }

        private MusicAttributes MusicAttributes;
        private MusicOrders MusicOrders;
        private WordList WordList;
        private MusicInfos MusicInfos;
        private List<NewSongData> AddedMusic { get; set; } = new List<NewSongData>();
        private BindingSource AddedMusicBinding { get; set; } = new BindingSource();
        private BindingSource LoadedMusicBinding { get; set; }
    }
}
