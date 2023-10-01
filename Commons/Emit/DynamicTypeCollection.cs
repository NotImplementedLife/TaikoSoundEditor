using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Commons.Emit
{
    internal class DynamicType
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("properties")] public EntityPropertyInfoDTO[] Properties { get; set; }

        [JsonPropertyName("interface")] public string Interface { get; set; } = null;

        public DynamicType() { }

        public DynamicType(string name, Type @interface, IEnumerable<EntityPropertyInfoDTO> properties)
        {
            Name = name;
            Interface = @interface.FullName;
            Properties = properties.ToArray();
        }
    }


    internal class DynamicTypeCollection
    {
        [JsonPropertyName("types")]
        public DynamicType[] Types { get; set; }        

        public DynamicTypeCollection() { }

        public DynamicTypeCollection(params DynamicType[] types)
        {
            Types = types;
        }
    }
}
