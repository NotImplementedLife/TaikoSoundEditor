using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Utils
{
    public static class Updates
    {
        public static async Task<GithubRelease> GetGithubLatestRelease(string owner, string repo)
        {
            var url = $"https://api.github.com/repos/{owner}/{repo}/releases/latest";
            using HttpClient client = new HttpClient();            
            string json = await client.GetStringAsync(url);

            var t = Task.Run(() => MessageBox.Show(json));
            t.Wait();

            return Json.Deserialize<GithubRelease>(json);
        }

        public static async Task<GithubRelease> GetLatestTja2Fumen()
        {
            return await GetGithubLatestRelease("vivaria", "tja2fumen");
        }


        public class GithubRelease
        {
            [JsonPropertyName("url")] public string Url { get; set; }
            [JsonPropertyName("tag_name")] public string TagName { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("prerelease")] public bool Prerelease { get; set; }
            [JsonPropertyName("assets")] public GithubAsset[] Assets { get; set; }

        }

        public class GithubAsset
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("browser_download_url")] public string DownloadUrl { get; set; }
        }
    }
}
