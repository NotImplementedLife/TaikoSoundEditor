using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;
using TaikoSoundEditor.Commons.IO;

namespace TaikoSoundEditor.Collections
{
    public static class Collections
    {
        public static Collection<T> FromJson<T>(string json, Type expectedItemType)
        {
            var colType = MakeGeneric(expectedItemType);
            Debug.WriteLine(json);

            var col = Json.Deserialize(colType, json);
            var items = colType.GetProperty("Items").GetValue(col) as IEnumerable;

            var result = new Collection<T>();
            foreach (var item in items)
            {
                if (item is T tItem) result.Items.Add(tItem);
                else throw new InvalidOperationException("Json parse error");
            }
            return result;
        }

        public static Type MakeGeneric(Type type) => typeof(Collection<>).MakeGenericType(type);
    }
    
    public class Collection<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new List<T>();                      
    }    
}
