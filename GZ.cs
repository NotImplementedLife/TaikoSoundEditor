using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor
{
    internal class GZ
    {
        public static string DecompressString(string gzPath)
        {
            using FileStream originalFileStream = File.OpenRead(gzPath);
            using GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);
            using StreamReader reader = new StreamReader(decompressionStream);
            return reader.ReadToEnd();
        }

        public static byte[] DecompressBytes(string gzPath)
        {
            using FileStream originalFileStream = File.OpenRead(gzPath);
            using GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);
            using MemoryStream ms = new MemoryStream();
            decompressionStream.CopyTo(ms);
            return ms.ToArray();
        }

        public static byte[] CompressToBytes(string content)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            writer.Write(content);
            using var ostream = new MemoryStream();
            using (var compressionStream = new GZipStream(ostream, CompressionMode.Compress)) 
            {
                stream.CopyTo(compressionStream);
            }

            return ostream.ToArray();
        }

        public static void CompressToFile(string fileName, string content)
        {
            using var stream = new MemoryStream();
            using var writer = new StreamWriter(stream);
            writer.Write(content);            
            using FileStream compressedFileStream = File.Create(fileName);
            using var compressor = new GZipStream(compressedFileStream, CompressionMode.Compress);
            new MemoryStream(stream.ToArray()).CopyTo(compressor);
        }
    }
}
