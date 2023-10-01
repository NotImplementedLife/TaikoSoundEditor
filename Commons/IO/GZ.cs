using ICSharpCode.SharpZipLib.GZip;
using System.IO;
using System.IO.Compression;
using System.Text;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.IO
{
    internal class GZ
    {

        public static string DecompressBytes(byte[] bytes)
        {
            Logger.Info("GZ Decompressing bytes");
            using (MemoryStream ms = new MemoryStream(bytes))
            using (GZipStream decompressionStream = new GZipStream(ms, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(decompressionStream))
                return reader.ReadToEnd();
        }

        public static string DecompressString(string gzPath)
        {
            Logger.Info("GZ Decompressing string");

            using (FileStream originalFileStream = File.OpenRead(gzPath))
            using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(decompressionStream))
                return reader.ReadToEnd();
        }

        public static byte[] DecompressBytes(string gzPath)
        {
            Logger.Info("GZ Decompressing bytes");
            using (FileStream originalFileStream = File.OpenRead(gzPath))
            using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
            using (MemoryStream ms = new MemoryStream())
            {
                decompressionStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static byte[] CompressToBytes(string content)
        {
            Logger.Info("GZ Compressing bytes");
            var uncompressed = Encoding.UTF8.GetBytes(content);
            using (MemoryStream outStream = new MemoryStream())
            {
                using (GZipOutputStream gzoStream = new GZipOutputStream(outStream))
                {
                    gzoStream.SetLevel(5);
                    gzoStream.Write(uncompressed, 0, uncompressed.Length);
                }
                return outStream.ToArray();
            }
        }       
    }
}
