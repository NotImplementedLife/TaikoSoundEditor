using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public static object Instantiate(Type type) => Activator.CreateInstance(MakeGeneric(type));
    }
    
    public class Collection<T>
    {
        [JsonPropertyName("items")]
        public List<T> Items { get; set; } = new List<T>();          
        
        public object Cast(Type type)
        {
            var col = Collections.Instantiate(type);
            var itemsProp = col.GetType().GetProperty("Items");
            var add = itemsProp.PropertyType.GetMethod("Add");
            var items = itemsProp.GetValue(col);
            foreach (var item in Items)
                add.Invoke(items, new object[] { Convert.ChangeType(item, type) });
                        
            return col;
        }
    }    
}
