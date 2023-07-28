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

            Logger.Info($"Setting LoadedMusicBox DataSource");

            LoadedMusicBinding = new BindingSource();
            LoadedMusicBinding.DataSource = MusicInfos.Items.Where(mi => mi.UniqueId != 0).ToList();
            LoadedMusicBox.DataSource = LoadedMusicBinding;
            TabControl.SelectedIndex = 1;
        });        

        #endregion                    
    }
}
