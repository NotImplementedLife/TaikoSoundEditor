using System.ComponentModel;
using System.Text.Json.Serialization;

namespace TaikoSoundEditor.Data
{
    public class MusicAttribute
    {
        [ReadOnly(true)]
        [JsonPropertyName("id")] public string Id { get; set; } = "ABCDEF";
        [ReadOnly(true)]
        [JsonPropertyName("uniqueId")] public int UniqueId { get; set; } 
        [JsonPropertyName("new")] public bool New { get; set; } = false;
        [JsonPropertyName("canPlayUra")] public bool CanPlayUra { get; set; } = false;

        [JsonPropertyName("doublePlay")] public bool DoublePlay { get; set; } = false;
        [JsonPropertyName("tag1")] public string Tag1 { get; set; } = "";
        [JsonPropertyName("tag2")] public string Tag2 { get; set; } = "";
        [JsonPropertyName("tag3")] public string Tag3 { get; set; } = "";
        [JsonPropertyName("tag4")] public string Tag4 { get; set; } = "";
        [JsonPropertyName("tag5")] public string Tag5 { get; set; } = "";
        [JsonPropertyName("tag6")] public string Tag6 { get; set; } = "";
        [JsonPropertyName("tag7")] public string Tag7 { get; set; } = "";
        [JsonPropertyName("tag8")] public string Tag8 { get; set; } = "";
        [JsonPropertyName("tag9")] public string Tag9 { get; set; } = "";
        [JsonPropertyName("tag10")] public string Tag10 { get; set; } = "";
        [JsonPropertyName("donBg1p")] public string DonBg1p { get; set; } = "";
        [JsonPropertyName("donBg2p")] public string DonBg2p { get; set; } = "";
        [JsonPropertyName("dancerDai")] public string DancerDai { get; set; } = "";
        [JsonPropertyName("dancer")] public string Dancer { get; set; } = "";
        [JsonPropertyName("danceNormalBg")] public string DanceNormalBg { get; set; } = "";
        [JsonPropertyName("danceFeverBg")] public string DanceFeverBg { get; set; } = "";
        [JsonPropertyName("rendaEffect")] public string RendaEffect { get; set; } = "";
        [JsonPropertyName("fever")] public string Fever { get; set; } = "";
        [JsonPropertyName("donBg1p1")] public string DonBg1p1 { get; set; } = "";
        [JsonPropertyName("donBg2p1")] public string DonBg2p1 { get; set; } = "";
        [JsonPropertyName("dancerDai1")] public string DancerDai1 { get; set; } = "";
        [JsonPropertyName("dancer1")] public string Dancer1 { get; set; } = "";
        [JsonPropertyName("danceNormalBg1")] public string DanceNormalBg1 { get; set; } = "";
        [JsonPropertyName("danceFeverBg1")] public string DanceFeverBg1 { get; set; } = "";
        [JsonPropertyName("rendaEffect1")] public string RendaEffect1 { get; set; } = "";
        [JsonPropertyName("fever1")] public string Fever1 { get; set; } = "";

        public MusicAttribute Clone()
        {
            var props = GetType().GetProperties();
            var clone = new MusicAttribute();
            foreach(var p in props)            
                p.SetValue(clone, p.GetValue(this));            
            return clone;
        }
    }
}
