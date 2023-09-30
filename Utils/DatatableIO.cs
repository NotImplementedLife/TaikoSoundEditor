namespace TaikoSoundEditor.Utils
{
    public class DatatableIO
    {
        public bool IsEncrypted { get; set; }        

        public T Deserialize<T>(string path)
        {
            if (!IsEncrypted)
            {                
                return Json.Deserialize<T>(GZ.DecompressString(path));
            }
            else
            {                
                return Json.Deserialize<T>(GZ.DecompressBytes(SSL.DecryptDatatable(File.ReadAllBytes(path))));
            }
        }

        public void Serialize<T>(string path, T item, bool indented = false, bool fixBools = false)
        {
            var str = JsonFix(Json.Serialize(item, indented));
            if(fixBools)
            {
                str = str
                    .Replace("\"new\": true,", "\"new\":true,")
                    .Replace("\"new\": false,", "\"new\":false,"); // is this still needed?
            }

            if (IsEncrypted)
                File.WriteAllBytes(path, SSL.EncryptDatatable(GZ.CompressToBytes(str)));
            else
                File.WriteAllBytes(path, GZ.CompressToBytes(str));
        }

        private static string JsonFix(string json)
        {
            var specialChars = "!@#$%^&*()_+=`~[]{}<>\\/'";
            foreach (var c in specialChars)
            {
                json = json.Replace($"\\u00{((int)c):X2}", $"{c}");
            }


            return json
                .Replace("\\u0022", "\\\"")
                .Replace("\r\n      ", "\r\n\t\t")
                .Replace("\r\n      ", "\r\n\t\t")
                .Replace("{\r\n  \"items\": [", "{\"items\":[")
                .Replace("    }", "\t}")
                .Replace("  ]\r\n}", "\t]\r\n}");
        }

    }
}
