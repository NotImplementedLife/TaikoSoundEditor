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
        public byte[] EBin1 { get; set; }
        public byte[] EBin2 { get; set; }
        public byte[] HBin { get; set; }
        public byte[] HBin1 { get; set; }
        public byte[] HBin2 { get; set; }
        public byte[] MBin { get; set; }
        public byte[] MBin1 { get; set; }
        public byte[] MBin2 { get; set; }
        public byte[] NBin { get; set; }
        public byte[] NBin1 { get; set; }
        public byte[] NBin2 { get; set; }
        public byte[] XBin { get; set; }        
        public byte[] XBin1 { get; set; }        
        public byte[] XBin2 { get; set; }        

        public byte[] Nus3Bank { get; set; }

        public IMusicAttribute MusicAttribute { get; set; }
        public IMusicInfo MusicInfo { get; set; }
        public IMusicOrder MusicOrder { get; set; }

        public IWord Word { get; set; }
        public IWord WordSub { get; set; }
        public IWord WordDetail { get; set; }


        public override string ToString() => $"{UniqueId}. {Id}";
    }
}
