using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaikoSoundEditor.Data;
using TaikoSoundEditor.Utils;

namespace TaikoSoundEditor
{
    partial class MainForm
    {
        #region Requesting Files

        private void WordListPathSelector_PathChanged(object sender, EventArgs args)
        {
            Logger.Info($"WordListPathSelector_PathChanged : {WordListPathSelector.Path}");
            WordListPath = WordListPathSelector.Path;
        }

        private void MusicInfoPathSelector_PathChanged(object sender, EventArgs args)
        {
            Logger.Info($"MusicInfoPathSelector_PathChanged : {MusicInfoPathSelector.Path}");
            MusicInfoPath = MusicInfoPathSelector.Path;
        }

        private void MusicOrderPathSelector_PathChanged(object sender, EventArgs args)
        {
            Logger.Info($"MusicOrderPathSelector_PathChanged : {MusicOrderPathSelector.Path}");
            MusicOrderPath = MusicOrderPathSelector.Path;
        }

        private void MusicAttributePathSelector_PathChanged(object sender, EventArgs args)
        {
            Logger.Info($"MusicAttributePathSelector_PathChanged : {MusicAttributePathSelector.Path}");
            MusicAttributePath = MusicAttributePathSelector.Path;
        }

        private void DirSelector_PathChanged(object sender, EventArgs args) => ExceptionGuard.Run(() =>
        {
            Logger.Info($"MusicAttributePathSelector_PathChanged : {DirSelector.Path}");
            var dir = DirSelector.Path;
            var files = new string[] { "music_attribute.bin", "music_order.bin", "musicinfo.bin", "wordlist.bin" };
            var sels = new PathSelector[] { MusicAttributePathSelector, MusicOrderPathSelector, MusicInfoPathSelector, WordListPathSelector };

            List<string> NotFoundFiles = new List<string>();

            for (int i = 0; i < files.Length; i++)
            {
                var path = Path.Combine(dir, files[i]);
                if (!File.Exists(path))
                {
                    NotFoundFiles.Add(files[i]);
                    continue;
                }
                sels[i].Path = path;
            }

            if (NotFoundFiles.Count > 0)
            {
                Logger.Warning("The following files could not be found:\n\n" + String.Join("\n", NotFoundFiles));
                MessageBox.Show("The following files could not be found:\n\n" + String.Join("\n", NotFoundFiles));
            }
        });

        private void OkButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            Logger.Info($"Clicked 'Looks good' ");

            try
            {
                MusicAttributes = Json.Deserialize<MusicAttributes>(GZ.DecompressString(MusicAttributePath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse\n{MusicAttributePath}\nReason:\n{ex.InnerException}");
            }
            try
            {
                MusicOrders = Json.Deserialize<MusicOrders>(GZ.DecompressString(MusicOrderPath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse\n{MusicOrderPath}\nReason:\n{ex.InnerException}");
            }
            try
            {
                MusicInfos = Json.Deserialize<MusicInfos>(GZ.DecompressString(MusicInfoPath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse\n{MusicInfoPath}\nReason:\n{ex.InnerException}");
            }
            try
            {
                WordList = Json.Deserialize<WordList>(GZ.DecompressString(WordListPath));
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to parse\n{WordListPath}\nReason:\n{ex.InnerException}");
            }

            Logger.Info($"Fixing Wordlist missing data");

            foreach (var mi in MusicInfos.Items)
            {
                var songId = mi.Id;

                if (WordList.GetBySong(songId) == null)
                    WordList.Items.Add(new Word { Key = $"song_{songId}", JapaneseText = "" });
                if (WordList.GetBySongSub(songId) == null)
                    WordList.Items.Add(new Word { Key = $"song_sub_{songId}", JapaneseText = "" });
                if (WordList.GetBySongDetail(songId) == null)
                    WordList.Items.Add(new Word { Key = $"song_detail_{songId}", JapaneseText = "" });                
            }

            Logger.Info($"Setting LoadedMusicBox DataSource");

            LoadedMusicBinding = new BindingSource();
            var cleanList = MusicInfos.Items.Where(mi => mi.UniqueId != 0).OrderBy(_ => _.UniqueId).ToList();

            LoadedMusicBinding.DataSource = cleanList;
            LoadedMusicBox.DataSource = LoadedMusicBinding;
            TabControl.SelectedIndex = 1;

            /*WordList.Items.RemoveAll(w =>
            {
                var key = w.Key;

                var prefixes = new string[] { "song_sub_", "song_detail_", "song_" };

                for (int i = 0; i < 3; i++)
                    if (key.StartsWith(prefixes[i]))
                    {
                        key = key.Substring(prefixes[i].Length);
                        break;
                    }
                return !MusicInfos.Items.Any(mi => mi.Id == key);
            });*/            

            MusicOrderViewer.WordList = WordList;
            foreach (var musicOrder in MusicOrders.Items.Where(_ => MusicInfos.Items.Any(mi => mi.UniqueId == _.UniqueId)))
            {
                MusicOrderViewer.AddSong(musicOrder);
            }
        });

        #endregion                    
    }
}
