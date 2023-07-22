using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Data
{
    internal class MusicInfo
    {
        [ReadOnly(true)]
        [JsonPropertyName("id")] public string Id { get; set; } = "ABCDEF";
        [ReadOnly(true)]
        [JsonPropertyName("uniqueId")] public int UniqueId { get; set; } =0;
        [JsonPropertyName("genreNo")] public int GenreNo { get; set; } =0;
        [JsonPropertyName("songFileName")] public string SongFileName { get; set; } = "";
        [JsonPropertyName("papamama")] public bool Papamama { get; set; } = false;
        [JsonPropertyName("branchEasy")] public bool BranchEasy { get; set; } = false;
        [JsonPropertyName("branchNormal")] public bool BranchNormal { get; set; } = false;
        [JsonPropertyName("branchHard")] public bool BranchHard { get; set; } = false;
        [JsonPropertyName("branchMania")] public bool BranchMania { get; set; } = false;
        [JsonPropertyName("branchUra")] public bool BranchUra { get; set; } = false;
        [JsonPropertyName("starEasy")] public int StarEasy { get; set; } = 0;
        [JsonPropertyName("starNormal")] public int StarNormal { get; set; } = 0;
        [JsonPropertyName("starHard")] public int StarHard { get; set; } = 0;
        [JsonPropertyName("starMania")] public int StarMania { get; set; } = 0;
        [JsonPropertyName("starUra")] public int StarUra { get; set; } = 0;
        [JsonPropertyName("shinutiEasy")] public int ShinutiEasy { get; set; } = 0;
        [JsonPropertyName("shinutiNormal")] public int ShinutiNormal { get; set; } = 0;
        [JsonPropertyName("shinutiHard")] public int ShinutiHard { get; set; } = 0;
        [JsonPropertyName("shinutiMania")] public int ShinutiMania { get; set; } = 0;
        [JsonPropertyName("shinutiUra")] public int ShinutiUra { get; set; } = 0;
        [JsonPropertyName("shinutiEasyDuet")] public int ShinutiEasyDuet { get; set; } = 0;
        [JsonPropertyName("shinutiNormalDuet")] public int ShinutiNormalDuet { get; set; } = 0;
        [JsonPropertyName("shinutiHardDuet")] public int ShinutiHardDuet { get; set; } = 0;
        [JsonPropertyName("shinutiManiaDuet")] public int ShinutiManiaDuet { get; set; } = 0;
        [JsonPropertyName("shinutiUraDuet")] public int ShinutiUraDuet { get; set; } = 0;
        [JsonPropertyName("shinutiScoreEasy")] public int ShinutiScoreEasy { get; set; } = 0;
        [JsonPropertyName("shinutiScoreNormal")] public int ShinutiScoreNormal { get; set; } = 0;
        [JsonPropertyName("shinutiScoreHard")] public int ShinutiScoreHard { get; set; } = 0;
        [JsonPropertyName("shinutiScoreMania")] public int ShinutiScoreMania { get; set; } = 0;
        [JsonPropertyName("shinutiScoreUra")] public int ShinutiScoreUra { get; set; } = 0;
        [JsonPropertyName("shinutiScoreEasyDuet")] public int ShinutiScoreEasyDuet { get; set; } = 0;
        [JsonPropertyName("shinutiScoreNormalDuet")] public int ShinutiScoreNormalDuet { get; set; } = 0;
        [JsonPropertyName("shinutiScoreHardDuet")] public int ShinutiScoreHardDuet { get; set; } = 0;
        [JsonPropertyName("shinutiScoreManiaDuet")] public int ShinutiScoreManiaDuet { get; set; } = 0;
        [JsonPropertyName("shinutiScoreUraDuet")] public int ShinutiScoreUraDuet { get; set; } = 0;
        [JsonPropertyName("easyOnpuNum")] public int EasyOnpuNum { get; set; } = 0;
        [JsonPropertyName("normalOnpuNum")] public int NormalOnpuNum { get; set; } = 0;
        [JsonPropertyName("hardOnpuNum")] public int HardOnpuNum { get; set; } = 0;
        [JsonPropertyName("maniaOnpuNum")] public int ManiaOnpuNum { get; set; } = 0;
        [JsonPropertyName("uraOnpuNum")] public int UraOnpuNum { get; set; } = 0;
        [JsonPropertyName("rendaTimeEasy")] public double RendaTimeEasy { get; set; } = 0;
        [JsonPropertyName("rendaTimeNormal")] public double RendaTimeNormal { get; set; } = 0;
        [JsonPropertyName("rendaTimeHard")] public double RendaTimeHard { get; set; } = 0;
        [JsonPropertyName("rendaTimeMania")] public double RendaTimeMania { get; set; } = 0;
        [JsonPropertyName("rendaTimeUra")] public double RendaTimeUra { get; set; } = 0;
        [JsonPropertyName("fuusenTotalEasy")] public int FuusenTotalEasy { get; set; } = 0;
        [JsonPropertyName("fuusenTotalNormal")] public int FuusenTotalNormal { get; set; } = 0;
        [JsonPropertyName("fuusenTotalHard")] public int FuusenTotalHard { get; set; } = 0;
        [JsonPropertyName("fuusenTotalMania")] public int FuusenTotalMania { get; set; } = 0;
        [JsonPropertyName("fuusenTotalUra")] public int FuusenTotalUra { get; set; } = 0;

        public override string ToString() => $"{UniqueId}. {Id}";


        [DefaultValue(Genre.Pop)]
        [JsonIgnore]
        public Genre Genre
        {
            get => (Genre)GenreNo;
            set => GenreNo = (int)value;
        }

        public MusicInfo Clone()
        {
            var props = GetType().GetProperties();
            var clone = new MusicInfo();
            foreach (var p in props)
                p.SetValue(clone, p.GetValue(this));
            return clone;
        }
    }    
}
