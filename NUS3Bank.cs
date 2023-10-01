using System.Drawing.Text;
using System.IO;
using System.Linq;
using TaikoSoundEditor.Commons.Utils;
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
            Logger.Info($"Creating NUS3BANK");
            Logger.Info($"songId = {songId}");
            Logger.Info($"uniqId = {uniqueId}");
            Logger.Info($"idsp.len = {idsp.Length}");
            Logger.Info($"demostart = {demostart}");
            using (var ms = new MemoryStream())
            {

                var header = Resources.song_ABCDEF_nus3bank.ToArray();


                Write32(header, 0x4, (uint)idsp.Length);
                for (int i = 0; i < songId.Length; i++)
                {
                    header[0xAA + i] = (byte)songId[i];
                    header[0x612 + i] = (byte)songId[i];
                }

                for (int i = songId.Length; i < 6; i++)
                {
                    header[0xAA + i] = 0;
                    header[0x612 + i] = 0;
                }


                header[0xB4] = (byte)(uniqueId & 0xFF);
                header[0xB5] = (byte)((uniqueId >> 8) & 0xFF);

                Write32(header, 0x4C, (uint)idsp.Length);
                Write32(header, 0x628, (uint)idsp.Length);
                Write32(header, 0x74C, (uint)idsp.Length);
                Write32(header, 0x4, (uint)idsp.Length);

                uint bb = (uint)(demostart * 1000);

                Write32(header, 0x6C4, bb);

                ms.Write(header);
                ms.Write(idsp);
                return ms.ToArray();
            }            
        }
        private static void Write(this MemoryStream ms, byte[] bytes) => ms.Write(bytes, 0, bytes.Length);
    }
}
