using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TaikoSoundEditor.Collections;
using TaikoSoundEditor.Commons.IO;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor.Commons.Utils
{
    public class DatatableIO
    {
        public bool IsEncrypted { get; set; }

        public T DeserializeCollection<T, I>(string path, Type itemType) where T: new()
        {
            if (!itemType.GetInterfaces().Any(_ => _ == typeof(I)))
                throw new ArgumentException($"Type {itemType} does not implement {typeof(I)}");

            var json = IsEncrypted
                ? GZ.DecompressBytes(SSL.DecryptDatatable(File.ReadAllBytes(path)))
                : GZ.DecompressString(path);
            var col = new T();

            var itemsProp = typeof(T).GetProperty("Items");
            var items = itemsProp.GetValue(col);

            var addRange = itemsProp.PropertyType.GetMethod("AddRange");
            addRange.Invoke(items, new object[] { Collections.Collections.FromJson<I>(json, itemType).Items });
            return col;
        }

        public WordList DeserializeWordList(string path)
        {
            return DeserializeCollection<WordList, IWord>(path, DatatableTypes.Word);            
        }


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

        public void DynamicSerialize(string path, object item, bool indented = false, bool fixBools = false)
        {
            var str = JsonFix(Json.DynamicSerialize(item, indented));
            if (fixBools)
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
