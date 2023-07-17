using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoSoundEditor.Properties;

namespace TaikoSoundEditor
{
    internal static class NUS3Bank
    {
        private static void Write32(byte[] b, int pos, uint x)
        {
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos] = (byte)(x & 0xFF);
        }

        public static byte[] GetNus3Bank(string songId, int uniqueId, byte[] idsp, float demostart)
        {
            using var ms = new MemoryStream();

            var header = Resources.song_ABCDEF_nus3bank.ToArray();
            for(int i=0;i<6;i++)
            {
                header[0xAA + i] = (byte)songId[i];
                header[0xD4E + i] = (byte)songId[i];
            }

            header[0xB4] = (byte)(uniqueId & 0xFF);
            header[0xB5] = (byte)((uniqueId >> 8) & 0xFF);

            Write32(header, 0x4C, (uint)idsp.Length);
            Write32(header, 0xD64, (uint)idsp.Length);
            Write32(header, 0xE1C, (uint)idsp.Length);

            uint bb = (uint)(demostart * 1000);

            Write32(header, 0xDDC, bb);
            Write32(header, 0xDE4, bb);

            ms.Write(header);
            ms.Write(idsp);
            return ms.ToArray();
        }
    }
}
