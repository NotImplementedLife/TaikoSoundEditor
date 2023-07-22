using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            MusicAttributePathSelector.PathChanged += MusicAttributePathSelector_PathChanged;
            MusicOrderPathSelector.PathChanged += MusicOrderPathSelector_PathChanged;
            MusicInfoPathSelector.PathChanged += MusicInfoPathSelector_PathChanged;
            WordListPathSelector.PathChanged += WordListPathSelector_PathChanged;
            DirSelector.PathChanged += DirSelector_PathChanged;

            AddedMusicBinding = new BindingSource();
            AddedMusicBinding.DataSource = AddedMusic;
            NewSoundsBox.DataSource = AddedMusicBinding;
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Info($"Commuted to tab {TabControl.SelectedIndex}");
        }

        private string MusicAttributePath { get; set; }
        private string MusicOrderPath { get; set; }
        private string MusicInfoPath { get; set; }
        private string WordListPath { get; set; }

        private MusicAttributes MusicAttributes;
        private MusicOrders MusicOrders;
        private WordList WordList;
        private MusicInfos MusicInfos;
        private List<NewSongData> AddedMusic { get; set; } = new List<NewSongData>();        
        private BindingSource AddedMusicBinding { get; set; }

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

        private void DirSelector_PathChanged(object sender, EventArgs args) => RunGuard(() =>
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

        private void OkButton_Click(object sender, EventArgs e) => RunGuard(() =>        
        {
            Logger.Info($"Clicked 'Looks good' ");

            try
            {
                MusicAttributes = Json.Deserialize<MusicAttributes>(GZ.DecompressString(MusicAttributePath));
            }
            catch(Exception ex)
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
            LoadedMusicBinding.DataSource = MusicInfos.Items;            
            LoadedMusicBox.DataSource = LoadedMusicBinding;            
            TabControl.SelectedIndex = 1;

        });

        BindingSource LoadedMusicBinding;

        #endregion        


        public static void RunGuard(Action action) 
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Error(ex);                
            }
        }

        public static void Error(Exception e)
        {
            MessageBox.Show(e.Message, "An error has occured");
            Logger.Error(e);
        }

        #region Editor

        private void GridsShow(MusicInfo item)
        {            
            Logger.Info($"Showing properties for MusicInfo: {item}");

            if(item==null)
            {
                MusicInfoGrid.SelectedObject = null;
                MusicAttributesGrid.SelectedObject = null;
                MusicOrderGrid.SelectedObject = null;
                WordsGrid.SelectedObject = null;
                WordSubGrid.SelectedObject = null;
                WordDetailGrid.SelectedObject = null;
                return;
            }

            MusicInfoGrid.SelectedObject = item;
            MusicAttributesGrid.SelectedObject = MusicAttributes.GetByUniqueId(item.UniqueId);
            MusicOrderGrid.SelectedObject = MusicOrders.GetByUniqueId(item.UniqueId);
            WordsGrid.SelectedObject = WordList.GetBySong(item.Id);
            WordSubGrid.SelectedObject = WordList.GetBySongSub(item.Id);
            WordDetailGrid.SelectedObject = WordList.GetBySongDetail(item.Id);
        }

        private bool indexChanging = false;

        private void LoadedMusicBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexChanging) return;
            indexChanging = true;
            NewSoundsBox.SelectedItem = null;
            var item = LoadedMusicBox.SelectedItem as MusicInfo;
            Logger.Info($"Selection Changed MusicItem: {item}");
            GridsShow(item);
            indexChanging = false;
        }

        private void EditorTable_Resize(object sender, EventArgs e)
        {
            WordsGB.Height = WordSubGB.Height = WordDetailGB.Height = EditorTable.Height / 3;
        }

        private void NewSoundsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexChanging) return;
            indexChanging = true;
            LoadedMusicBox.SelectedItem = null;   
            var item = NewSoundsBox.SelectedItem as NewSongData;
            Logger.Info($"Selection Changed NewSongData: {item}");
            Logger.Info($"Showing properties for NewSongData: {item}");
            MusicInfoGrid.SelectedObject = item?.MusicInfo;
            MusicAttributesGrid.SelectedObject = item?.MusicAttribute;
            MusicOrderGrid.SelectedObject = item?.MusicOrder;
            WordsGrid.SelectedObject = item?.Word;
            WordSubGrid.SelectedObject = item?.WordSub;
            WordDetailGrid.SelectedObject = item?.WordDetail;
            indexChanging = false;
        }

        #endregion

        private void CreateButton_Click(object sender, EventArgs e)
        {
            Logger.Info($"Clicked Create Button");
            AudioFileSelector.Path = "";
            TJASelector.Path = "";
            SongNameBox.Text = "(6 characters id...)";
            TabControl.SelectedIndex = 2;
        }

        private void CreateBackButton_Click(object sender, EventArgs e)
        {
            Logger.Info($"Clicked Back Button");
            TabControl.SelectedIndex = 1;
        }

        
        private void WarnWithBox(string message)
        {
            Logger.Warning("Displayed: " + message);
            MessageBox.Show(message);
        }

        private void CreateOkButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info($"Clicked Ok Button");
            FeedbackBox.Clear();
            var audioFilePath = AudioFileSelector.Path;
            var tjaPath = TJASelector.Path;
            var songName = SongNameBox.Text;
            var id = Math.Max(MusicAttributes.GetNewId(), AddedMusic.Count == 0 ? 0 : AddedMusic.Max(_ => _.UniqueId) + 1);

            Logger.Info($"Audio File = {audioFilePath}");
            Logger.Info($"TJA File = {tjaPath}");
            Logger.Info($"Song Name (Id) = {songName}");
            Logger.Info($"UniqueId = {id}");

            if (songName == null || songName.Length == 0 || songName.Length > 6) 
            {
                WarnWithBox("Invalid song name.");                
                return;
            }

            if (!MusicAttributes.IsValidSongId(songName) || AddedMusic.Any(m => m.Id == songName))
            {
                WarnWithBox("Duplicate song name. Choose another");
                return;
            }

            FeedbackBox.AppendText("Creating temp dir\r\n");
            
            CreateTmpDir();

            FeedbackBox.AppendText("Parsing TJA\r\n");

            Logger.Info("Parsing TJA");

            var tja = TjaEncAuto.Checked ? TJA.ReadDefault(tjaPath)
                    : TjaEncUTF8.Checked ? TJA.ReadAsUTF8(tjaPath)
                    : TJA.ReadAsShiftJIS(tjaPath);
                        
            File.WriteAllText("tja.txt", tja.ToString());


            var seconds = AddSilenceBox.Checked ? (int)Math.Ceiling(tja.Headers.Offset + 3) : 0;
            if (seconds < 0) seconds = 0;
            

            FeedbackBox.AppendText("Converting to wav\r\n");            
            WAV.ConvertToWav(audioFilePath, $@".-tmp\{songName}.wav", seconds);


            Logger.Info("Adjusting seconds of silence");
            tja.Headers.Offset -= seconds;
            tja.Headers.DemoStart += seconds;

            var text = File.ReadAllLines(tjaPath);

            text = text.Select(l =>
            {
                if (l.StartsWith("OFFSET:"))
                    return $"OFFSET:{tja.Headers.Offset:n3}";
                if (l.StartsWith("DEMOSTART:"))
                    return $"DEMOSTART:{tja.Headers.DemoStart:n3}";
                return l;
            }).ToArray();

            Logger.Info("Creating temporary tja");
            var newTja = @$".-tmp\{Path.GetFileName(tjaPath)}";
            File.WriteAllLines(newTja, text);


            FeedbackBox.AppendText("Running tja2fumen\r\n");

            var tja_binaries = TJA.RunTja2Fumen(newTja);

            Logger.Info("Creating new sonud data");
            FeedbackBox.AppendText("Creating sound data\r\n");
            NewSongData ns = new NewSongData();

            ns.UniqueId = id;
            ns.Id = songName;
            
            ns.Wav = File.ReadAllBytes($@".-tmp\{songName}.wav");
            ns.EBin = tja_binaries[0];
            ns.HBin = tja_binaries[1];
            ns.MBin = tja_binaries[2];
            ns.NBin = tja_binaries[3];            

            var selectedMusicInfo = LoadedMusicBox.SelectedItem as MusicInfo;            

            var mi = (selectedMusicInfo ?? (NewSoundsBox.SelectedItem as NewSongData)?.MusicInfo ?? new MusicInfo()).Clone();
            mi.Id = songName;
            mi.UniqueId = id;
            ns.MusicInfo = mi;

            var mo = new MusicOrder();
            mo.Id = songName;
            mo.UniqueId = id;
            ns.MusicOrder = mo;

            var ma = selectedMusicInfo != null ? MusicAttributes.GetByUniqueId(selectedMusicInfo.UniqueId).Clone() : new MusicAttribute();
            ma.Id = songName;
            ma.UniqueId = id;
            ma.New = true;
            ns.MusicAttribute = ma;            

            ns.Word = new Word { Key = $"song_{songName}", JapaneseText = tja.Headers.Title };
            ns.WordSub = new Word { Key = $"song_sub_{songName}", JapaneseText = tja.Headers.Subtitle };
            ns.WordDetail = new Word { Key = $"song_detail_{songName}", JapaneseText = tja.Headers.TitleJa };

            mi.EasyOnpuNum = tja.Courses[0].NotesCount;
            mi.NormalOnpuNum = tja.Courses[1].NotesCount;
            mi.HardOnpuNum = tja.Courses[2].NotesCount;
            mi.ManiaOnpuNum = tja.Courses[3].NotesCount;

            mi.StarEasy = tja.Courses[0].Headers.Level; 
            mi.StarNormal = tja.Courses[1].Headers.Level; 
            mi.StarHard = tja.Courses[2].Headers.Level; 
            mi.StarMania = tja.Courses[3].Headers.Level;

            if (tja.Courses.ContainsKey(4)) 
            {
                FeedbackBox.AppendText("URA course detected\r\n");
                mi.UraOnpuNum = tja.Courses[4].NotesCount;
                mi.StarUra = tja.Courses[4].Headers.Level;
                ma.CanPlayUra = true;                

            }
            else
            {
                ma.CanPlayUra = false;
            }

            FeedbackBox.AppendText("Adjusting shinuti\r\n");

            mi.ShinutiEasy = (mi.ShinutiScoreEasy / mi.EasyOnpuNum) / 10 * 10;
            mi.ShinutiNormal = (mi.ShinutiScoreNormal / mi.NormalOnpuNum) / 10 * 10;
            mi.ShinutiHard = (mi.ShinutiScoreHard / mi.HardOnpuNum) / 10 * 10;
            mi.ShinutiMania = (mi.ShinutiScoreMania / mi.ManiaOnpuNum) / 10 * 10;

            mi.ShinutiEasyDuet = (mi.ShinutiScoreEasyDuet / mi.EasyOnpuNum) / 10 * 10;
            mi.ShinutiNormalDuet = (mi.ShinutiScoreNormalDuet / mi.NormalOnpuNum) / 10 * 10;
            mi.ShinutiHardDuet = (mi.ShinutiScoreHardDuet / mi.HardOnpuNum) / 10 * 10;
            mi.ShinutiManiaDuet = (mi.ShinutiScoreManiaDuet / mi.ManiaOnpuNum) / 10 * 10;

            if (ma.CanPlayUra)
            {
                mi.ShinutiScoreUra = 1002320;
                mi.ShinutiScoreUraDuet = 1002320;
                mi.ShinutiUra = (mi.ShinutiScoreUra / mi.UraOnpuNum) / 10 * 10;
                mi.ShinutiUraDuet = (mi.ShinutiScoreUraDuet / mi.UraOnpuNum) / 10 * 10;
            }

            if(ma.CanPlayUra)
            {
                ns.XBin = tja_binaries[4];
            }

            mi.SongFileName = $"sound/song_{songName}";

            mi.RendaTimeEasy = 0;
            mi.RendaTimeHard = 0;
            mi.RendaTimeMania = 0;
            mi.RendaTimeNormal = 0;
            mi.RendaTimeUra = 0;
            mi.FuusenTotalEasy = 0;
            mi.FuusenTotalHard = 0;
            mi.FuusenTotalMania = 0;
            mi.FuusenTotalNormal = 0;
            mi.FuusenTotalUra = 0;

            Dictionary<string, int> genres = new Dictionary<string, int>
            {
                { "POP", 0 },
                { "ANIME", 1 },
                { "KIDS", 2 },
                { "VOCALOID", 3 },
                { "GAME MUSIC", 4 },
                { "NAMCO ORIGINAL", 5 },
            };

            if (tja.Headers.Genre != null && genres.ContainsKey(tja.Headers.Genre.ToUpper()))
                mi.GenreNo = genres[tja.Headers.Genre.ToUpper()];

            FeedbackBox.AppendText("Converting to idsp\r\n");            
            IDSP.WavToIdsp($@".-tmp\{songName}.wav", $@".-tmp\{songName}.idsp");            

            var idsp = File.ReadAllBytes($@".-tmp\{songName}.idsp");


            FeedbackBox.AppendText("Creating nus3bank file\r\n");

            ns.Nus3Bank = NUS3Bank.GetNus3Bank(songName, id, idsp, tja.Headers.DemoStart);


            Logger.Info("Conversion done");
            FeedbackBox.AppendText("Done\r\n");

            AddedMusic.Add(ns);
            AddedMusicBinding.ResetBindings(false);

            NewSoundsBox.ClearSelected();
            NewSoundsBox.SelectedItem = ns;            

            TabControl.SelectedIndex = 1;
            FeedbackBox.Clear();
        });

        private void CreateTmpDir()
        {
            Logger.Info($"Creating .-tmp/");
            if (!Directory.Exists(".-tmp"))            
                Directory.CreateDirectory(".-tmp");            
        }

        private string JsonFix(string json)
        {
            var specialChars = "!@#$%^&*()_+=`~[]{}<>\\/'";
            foreach(var c in specialChars)
            {
                json = json.Replace($"\\u00{((int)c):X2}", $"{c}");
            }


            return json
                .Replace("\\u0022", "\\\"")
                .Replace("\r\n      ", "\r\n\t\t")
                .Replace("\r\n      ", "\r\n\t\t")
                .Replace("{\r\n  \"items\": [", "{\"items\":[")
                .Replace("    }", "\t}")
                .Replace("  ]\r\n}", "\t]\r\n}");
        }

        private void ExportDatatable(string path)
        {
            Logger.Info($"Exporting Datatable to '{path}'");
            var mi = new MusicInfos();
            mi.Items.AddRange(MusicInfos.Items);
            mi.Items.AddRange(AddedMusic.Select(_ => _.MusicInfo));

            var ma = new MusicAttributes();
            ma.Items.AddRange(MusicAttributes.Items);
            ma.Items.AddRange(AddedMusic.Select(_ => _.MusicAttribute));            

            var mo = new MusicOrders();

            var mbyg = MusicOrders.Items.GroupBy(_ => _.GenreNo).Select(_ => (_.Key, List: _.ToList())).ToDictionary(_ => _.Key, _ => _.List);

            foreach (var m in AddedMusic.Select(_ => _.MusicOrder))
            {
                if (!mbyg.ContainsKey(m.GenreNo))
                    mbyg[m.GenreNo] = new List<MusicOrder>();
                mbyg[m.GenreNo] = mbyg[m.GenreNo].Prepend(m).ToList();
            }

            foreach(var key in mbyg.Keys.OrderBy(_=>_))
            {
                mo.Items.AddRange(mbyg[key]);
            }            

            var wl = new WordList();
            wl.Items.AddRange(WordList.Items);
            wl.Items.AddRange(AddedMusic.Select(_ => new List<Word>() { _.Word, _.WordSub, _.WordDetail }).SelectMany(_ => _));

            var jmi = JsonFix(Json.Serialize(mi, !DatatableSpaces.Checked));
            var jma = JsonFix(Json.Serialize(ma));
            var jmo = JsonFix(Json.Serialize(mo));
            var jwl = JsonFix(Json.Serialize(wl, !DatatableSpaces.Checked));

            jma = jma.Replace("\"new\": true,", "\"new\":true,");
            jma = jma.Replace("\"new\": false,", "\"new\":false,");

            File.WriteAllText(Path.Combine(path,"musicinfo"), jmi);
            File.WriteAllText(Path.Combine(path,"music_attribute"), jma);
            File.WriteAllText(Path.Combine(path,"music_order"), jmo);
            File.WriteAllText(Path.Combine(path, "wordlist"), jwl);

            GZ.CompressToFile(Path.Combine(path,"musicinfo.bin"), jmi);
            GZ.CompressToFile(Path.Combine(path, "music_attribute.bin"), jma);
            GZ.CompressToFile(Path.Combine(path, "music_order.bin"), jmo);
            GZ.CompressToFile(Path.Combine(path, "wordlist.bin"), jwl);
        }

        private void ExportNusBanks(string path)
        {
            Logger.Info($"Exporting NUS3BaNKS to '{path}'");
            foreach (var ns in AddedMusic)
            {
                File.WriteAllBytes(Path.Combine(path, $"song_{ns.Id}.nus3bank"), ns.Nus3Bank);
            }
        }

        private void ExportSoundBinaries(string path)
        {
            Logger.Info($"Exporting Sound .bin's to '{path}'");
            foreach (var ns in AddedMusic)
            {
                var sdir = Path.Combine(path, ns.Id);

                if (!Directory.Exists(sdir))
                    Directory.CreateDirectory(sdir);

                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_e.bin"), ns.EBin);                
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_n.bin"), ns.NBin);                
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_h.bin"), ns.HBin);                
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_m.bin"), ns.MBin);

                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_e_1.bin"), ns.EBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_n_1.bin"), ns.NBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_h_1.bin"), ns.HBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_m_1.bin"), ns.MBin);

                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_e_2.bin"), ns.EBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_n_2.bin"), ns.NBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_h_2.bin"), ns.HBin);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_m_2.bin"), ns.MBin);

                if(ns.MusicAttribute.CanPlayUra)
                {
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x.bin"), ns.XBin);
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x_1.bin"), ns.XBin);
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x_2.bin"), ns.XBin);
                }                
            }
        }


        private void ExportDatatableButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info($"Clicked ExportDatatableButton");
            var path = PickPath();
            if (path == null)
            {
                MessageBox.Show("No path chosen. Operation canceled");
                return;
            }
            ExportDatatable(path);
            MessageBox.Show("Done");            
            if (ExportOpenOnFinished.Checked)
                Process.Start($"explorer.exe", path);
        });                

        private void ExportSoundFoldersButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info($"Clicked ExportSoundFoldersButton");
            var path = PickPath();
            if (path == null)
            {
                MessageBox.Show("No path chosen. Operation canceled");
                return;
            }
            ExportSoundBinaries(path);
            MessageBox.Show("Done");            
            if (ExportOpenOnFinished.Checked)
                Process.Start($"explorer.exe", path);
        });

        private void ExportSoundBanksButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info($"Clicked ExportSoundBanksButton");
            var path = PickPath();
            if (path == null)
            {
                MessageBox.Show("No path chosen. Operation canceled");
                return;
            }
            ExportNusBanks(path);
            MessageBox.Show("Done");            
            if (ExportOpenOnFinished.Checked)
                Process.Start($"explorer.exe", path);
        });

        private string PickPath()
        {
            Logger.Info($"Picking path dialog");
            var picker = new FolderPicker();
            if (picker.ShowDialog() == true)
                return picker.ResultPath;
            return null;
        }

        private void ExportAllButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info($"Clicked Export All");
            var path = PickPath();
            if (path == null)
            {
                MessageBox.Show("No path chosen. Operation canceled");
                return;
            }

            var dtpath = Path.Combine(path, "datatable");
            if (!Directory.Exists(dtpath)) Directory.CreateDirectory(dtpath);

            var nupath = Path.Combine(path, "nus3banks");
            if (!Directory.Exists(nupath)) Directory.CreateDirectory(nupath);

            var sbpath = Path.Combine(path, "soundsbin");
            if (!Directory.Exists(sbpath)) Directory.CreateDirectory(sbpath);

            ExportDatatable(dtpath);
            ExportSoundBinaries(sbpath);
            ExportNusBanks(nupath);
            MessageBox.Show("Done");            
            if (ExportOpenOnFinished.Checked)
                Process.Start($"explorer.exe", path);

        });

        private void TJASelector_PathChanged(object sender, EventArgs args)
        {
            if (TJASelector.Path == null) return;
            if (SongNameBox.Text != "" && SongNameBox.Text != "(6 characters id...)") return;

            var name = Path.GetFileNameWithoutExtension(TJASelector.Path);
            if (name.Length == 6)
                SongNameBox.Text = name;
        }

        private void RemoveSongButton_Click(object sender, EventArgs e) => RunGuard(() =>
        {
            Logger.Info("Clicked remove song");
            if (NewSoundsBox.SelectedItem != null)
            {
                Logger.Info("Removing newly added song");
                AddedMusic.Remove(NewSoundsBox.SelectedItem as NewSongData);
                Logger.Info("Refreshing list");
                AddedMusicBinding.ResetBindings(false);
                return;
            }

            if (LoadedMusicBox.SelectedItem != null)
            {
                Logger.Info("Removing existing song");
                var mi = LoadedMusicBox.SelectedItem as MusicInfo;
                var ma = MusicAttributes.GetByUniqueId(mi.UniqueId);
                var mo = MusicOrders.GetByUniqueId(mi.UniqueId);
                var w = WordList.GetBySong(mi.Id);
                var ws = WordList.GetBySongSub(mi.Id);
                var wd = WordList.GetBySongDetail(mi.Id);

                Logger.Info("Removing music info");
                MusicInfos.Items.RemoveAll(x => x.UniqueId == mi.UniqueId);
                Logger.Info("Removing music attribute");
                MusicAttributes.Items.Remove(ma);
                Logger.Info("Removing music order");
                MusicOrders.Items.Remove(mo);
                Logger.Info("Removing word");
                WordList.Items.Remove(w);
                Logger.Info("Removing word sub");
                WordList.Items.Remove(ws);
                Logger.Info("Removing word detail");
                WordList.Items.Remove(wd);

                Logger.Info("Refreshing list");
                LoadedMusicBinding.ResetBindings(false);
                return;
            }
        });
    }
}