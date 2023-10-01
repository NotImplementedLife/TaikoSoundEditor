using System;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.Emit
{
    internal class EntityPropertyInfoDTO
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("type")] public string Type { get; set; }
        [JsonPropertyName("jsonName")] public string JsonName { get; set; }
        [JsonPropertyName("isReadOnly")] public bool IsReadOnly { get; set; } = false;
        [JsonPropertyName("defaultValue")] public string DefaultValue { get; set; } = null;

        public static EntityPropertyInfoDTO FromPropertyInfo(EntityPropertyInfo p)
            => new EntityPropertyInfoDTO
            {
                Name = p.Name,
                Type = p.Type.Name,
                JsonName = p.JsonPropertyName,
                IsReadOnly = p.IsReadOnly,
                DefaultValue = p.DefaultValue?.ToString() ?? null
            };

        public EntityPropertyInfo CreatePropertyInfo()
        {
            var type = Types.GetTypeByName(Type);
            if (type == null)
                throw new InvalidOperationException($"Cannot find type `{Type}`");
            return new EntityPropertyInfo(Name, type, JsonName, IsReadOnly, GetDefaultValue(type, DefaultValue));
        }

        private static object GetDefaultValue(Type type, string valStr)
        {
            if (valStr == null) return type.IsValueType ? Activator.CreateInstance(type) : null;
            if (type == typeof(int)) return int.Parse(valStr);
            if (type == typeof(uint)) return uint.Parse(valStr);
            if (type == typeof(short)) return short.Parse(valStr);
            if (type == typeof(ushort)) return ushort.Parse(valStr);
            if (type == typeof(string)) return valStr;
            if (type == typeof(bool)) return (valStr.ToLower() == "true" || valStr == "1") ? true : false;
            if (type == typeof(double)) return double.Parse(valStr);

            throw new InvalidOperationException($"Cannot decode default value of type {type}");
        }    
    }
}
