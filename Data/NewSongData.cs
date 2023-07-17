using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    internal class NewSongData
    {
        public string Id { get; set; }
        public int UniqueId { get; set; }
        public byte[] Wav { get; set; }

        public byte[] EBin { get; set; }
        public byte[] HBin { get; set; }
        public byte[] MBin { get; set; }
        public byte[] NBin { get; set; }
        public byte[] XBin { get; set; }        

        public byte[] Nus3Bank { get; set; }

        public MusicAttribute MusicAttribute { get; set; }
        public MusicInfo MusicInfo { get; set; }
        public MusicOrder MusicOrder { get; set; }

        public Word Word { get; set; }
        public Word WordSub { get; set; }
        public Word WordDetail { get; set; }


        public override string ToString() => $"{UniqueId}. {Id}";
    }
}
