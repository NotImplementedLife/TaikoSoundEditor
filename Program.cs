using System.Linq;
using TaikoNus3BankTemplateFix.Properties;

namespace TaikoNus3BankTemplateFix
{
    internal class Program
    {
        static int Similarity(byte[] buff, byte[] template)
        {
            if (buff.Length < template.Length) return 0;
            int c = 0;
            for (int i = 0; i < template.Length; i++) 
            {
                if (buff[i] == template[i]) c++;
            }
            return c * 100 / template.Length;
        }

        private static void Write32(byte[] b, int pos, uint x)
        {
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos] = (byte)(x & 0xFF);
        }

        private static int Read32(byte[] b, int pos)
        {
            int x = 0;
            x |= b[pos++];
            x |= b[pos++] << 8;
            x |= b[pos++] << 16;
            x |= b[pos++] << 24;
            return x;
        }

        private static void Write16(byte[] b, int pos, uint x)
        {
            b[pos++] = (byte)(x & 0xFF); x >>= 8;
            b[pos++] = (byte)(x & 0xFF);
        }

        private static int Read16(byte[] b, int pos)
        {
            int x = 0;
            x |= b[pos++];
            x |= b[pos++] << 8;            
            return x;
        }

        private static byte[] ReadBytes(byte[] b, int pos, int count)
        {
            var result = new byte[count];
            for (int i = 0; i < count; i++)            
                result[i] = b[pos++];            
            return result;
        }

        private static void WriteBytes(byte[] b, int pos, byte[] src, int count)
        {
            for(int i=0;i<count;i++)            
                b[pos++] = src[i];            
        }

        static void Main(string[] args)
        {
            bool force = args.Contains("-f");
            var new_args = args.Where(a => !a.StartsWith("-"));

            foreach (var file in new_args) 
            {                
                Console.WriteLine(file);

                try
                {
                    var input = File.ReadAllBytes(file);
                    var new_similarity = Similarity(input, Resources.song_ABCDEF_nus3bank);

                    if (new_similarity > 90)
                        continue;

                    if (Similarity(input, Resources.song_ABCDEF_nus3bank_old) < 90 && !force)
                    {
                        Console.WriteLine("This file doesn't resemble the old template. Use -f option to force it being converted to the new template (only if you know what you're doing).");
                        continue;
                    }

                    // once we are sure that the nus3bank is in the old tempalte, we can convert it to the new one

                    var idsp = input.Skip(Resources.song_ABCDEF_nus3bank_old.Length).ToArray();

                    var length = Read32(input, 0x4);

                    if (length != idsp.Length)
                    {
                        Console.WriteLine("Inconsistency fonud when getting IDSP length. Skipping the file...");
                        continue;
                    }

                    var songName = ReadBytes(input, 0xAA, 6);

                    var uniqId = Read16(input, 0xB4);

                    int bb = Read32(input, 0xDDC);

                    var header = Resources.song_ABCDEF_nus3bank.ToArray();

                    Write32(header, 0x4, (uint)idsp.Length);
                    WriteBytes(header, 0xAA, songName, 6);
                    WriteBytes(header, 0x612, songName, 6);
                    Write16(header, 0xB4, (uint)uniqId);
                    Write32(header, 0x4C, (uint)idsp.Length);
                    Write32(header, 0x628, (uint)idsp.Length);
                    Write32(header, 0x74C, (uint)idsp.Length);
                    Write32(header, 0x4, (uint)idsp.Length);
                    Write32(header, 0x6C4, (uint)bb);
                    File.WriteAllBytes(file, header.Concat(idsp).ToArray());
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error");
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}