using System;
using System.Text.Json.Serialization;

namespace TaikoSoundEditor.Commons.Emit
{
    internal class EntityPropertyInfo
    {        
        public string Name { get; }
        public Type Type { get; }
        public string JsonPropertyName { get; }
        public bool IsReadOnly { get; }
        public object DefaultValue { get; }        

        public EntityPropertyInfo(string name, Type type, string jsonPropertyName = null, bool isReadOnly = false, object defaultValue = null)
        {
            Name = name;
            Type = type;
            JsonPropertyName = jsonPropertyName ?? char.ToLower(Name[0]) + Name.Substring(1);
            IsReadOnly = isReadOnly;
            DefaultValue = defaultValue;
        }        
    }
}
