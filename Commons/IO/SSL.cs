using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.IO
{
    internal static class SSL
    {
        private static byte[] GetKeyFromString(string str)
        {
            if (str == null) return null;
            if (Regex.IsMatch(str, @"^[0-9a-fA-F]{64}$")) 
            {
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < 64; i += 2) 
                    bytes.Add(byte.Parse($"{str[i]}{str[i + 1]}", System.Globalization.NumberStyles.HexNumber));
                return bytes.ToArray();
            }
            return null;
        }

        public static void LoadKeys()
        {            
            DatatableKey = GetKeyFromString(Config.DatatableKey);
            FumenKey = GetKeyFromString(Config.FumenKey);
        }

        private static byte[] DatatableKey = null;
        private static byte[] FumenKey = null;

        public static byte[] DecryptFumen(byte[] data) => Decrypt(data, ExpectKey(FumenKey, "Fumen"));
        public static byte[] EncryptFumen(byte[] data) => Encrypt(data, ExpectKey(FumenKey, "Fumen"));

        public static byte[] DecryptDatatable(byte[] data) => Decrypt(data, ExpectKey(DatatableKey, "Datatable"));
        public static byte[] EncryptDatatable(byte[] data) => Encrypt(data, ExpectKey(DatatableKey, "Datatable"));

        // https://gist.github.com/mhingston/a47caa21298950abc4d8422d98b7437e
        public static byte[] Encrypt(byte[] bytes, byte[] key)
        {
            byte[] cipherData;
            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = new byte[16];
            //aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);                    
                }
                cipherData = ms.ToArray();
            }

            byte[] combinedData = new byte[aes.IV.Length + cipherData.Length];
            Array.Copy(aes.IV, 0, combinedData, 0, aes.IV.Length);
            Array.Copy(cipherData, 0, combinedData, aes.IV.Length, cipherData.Length);
            return combinedData;
        }

        public static byte[] Decrypt(byte[] combinedData, byte[] key)
        {                       
            Aes aes = Aes.Create();
            aes.Key = key;
            byte[] iv = new byte[aes.BlockSize / 8];
            byte[] cipherText = new byte[combinedData.Length - iv.Length];
            Array.Copy(combinedData, iv, iv.Length);
            Array.Copy(combinedData, iv.Length, cipherText, 0, cipherText.Length);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;            
            ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);
            using (MemoryStream ms = new MemoryStream(cipherText))
            using (CryptoStream cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read))
            using (MemoryStream rs = new MemoryStream())
            {
                cs.CopyTo(rs);
                return rs.ToArray();
            }                
        }

        private static byte[] ExpectKey(byte[] key, string keyName)
        {
            //if (key != null) Debug.WriteLine(string.Join(", ", key.Select(_ => _.ToString("X2"))));
            return key ?? throw new InvalidOperationException($"{keyName} key is needed for the requested operation");
        }
    }
}
