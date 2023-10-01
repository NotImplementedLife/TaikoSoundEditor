using System.Text.Json.Serialization;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Data
{
    public interface IMusicInfo
    {        
        string Id { get; set; }         
        int UniqueId { get; set; } 
        int GenreNo { get; set; } 
        string SongFileName { get; set; } 
        bool Papamama { get; set; } 
        bool BranchEasy { get; set; } 
        bool BranchNormal { get; set; } 
        bool BranchHard { get; set; } 
        bool BranchMania { get; set; } 
        bool BranchUra { get; set; } 
        int StarEasy { get; set; } 
        int StarNormal { get; set; } 
        int StarHard { get; set; } 
        int StarMania { get; set; } 
        int StarUra { get; set; } 
        int ShinutiEasy { get; set; } 
        int ShinutiNormal { get; set; } 
        int ShinutiHard { get; set; } 
        int ShinutiMania { get; set; } 
        int ShinutiUra { get; set; } 
        int ShinutiEasyDuet { get; set; } 
        int ShinutiNormalDuet { get; set; } 
        int ShinutiHardDuet { get; set; } 
        int ShinutiManiaDuet { get; set; } 
        int ShinutiUraDuet { get; set; } 
        int ShinutiScoreEasy { get; set; } 
        int ShinutiScoreNormal { get; set; } 
        int ShinutiScoreHard { get; set; } 
        int ShinutiScoreMania { get; set; } 
        int ShinutiScoreUra { get; set; } 
        int ShinutiScoreEasyDuet { get; set; } 
        int ShinutiScoreNormalDuet { get; set; } 
        int ShinutiScoreHardDuet { get; set; } 
        int ShinutiScoreManiaDuet { get; set; } 
        int ShinutiScoreUraDuet { get; set; } 
        int EasyOnpuNum { get; set; } 
        int NormalOnpuNum { get; set; } 
        int HardOnpuNum { get; set; } 
        int ManiaOnpuNum { get; set; } 
        int UraOnpuNum { get; set; } 
        double RendaTimeEasy { get; set; } 
        double RendaTimeNormal { get; set; } 
        double RendaTimeHard { get; set; } 
        double RendaTimeMania { get; set; } 
        double RendaTimeUra { get; set; } 
        int FuusenTotalEasy { get; set; } 
        int FuusenTotalNormal { get; set; } 
        int FuusenTotalHard { get; set; } 
        int FuusenTotalMania { get; set; } 
        int FuusenTotalUra { get; set; }


        [Recast("GenreNo")]
        [JsonIgnore]
        Genre Genre { get; set; }
    }
}
