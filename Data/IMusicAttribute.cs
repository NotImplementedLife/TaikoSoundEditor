namespace TaikoSoundEditor.Data
{
    public interface IMusicAttribute
    {
        string Id { get; set; }
        int UniqueId { get; set; }
        bool New { get; set; }
        bool CanPlayUra { get; set; }
        bool DoublePlay { get; set; }
        string Tag1 { get; set; }
        string Tag2 { get; set; }
        string Tag3 { get; set; }
        string Tag4 { get; set; }
        string Tag5 { get; set; }
        string Tag6 { get; set; }
        string Tag7 { get; set; }
        string Tag8 { get; set; }
        string Tag9 { get; set; }
        string Tag10 { get; set; }
        string DonBg1p { get; set; }
        string DonBg2p { get; set; }
        string DancerDai { get; set; }
        string Dancer { get; set; }
        string DanceNormalBg { get; set; }
        string DanceFeverBg { get; set; }
        string RendaEffect { get; set; }
        string Fever { get; set; }
        string DonBg1p1 { get; set; }
        string DonBg2p1 { get; set; }
        string DancerDai1 { get; set; }
        string Dancer1 { get; set; }
        string DanceNormalBg1 { get; set; }
        string DanceFeverBg1 { get; set; }
        string RendaEffect1 { get; set; }
        string Fever1 { get; set; }
    }
}
