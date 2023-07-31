using TaikoSoundEditor.Data;
using TaikoSoundEditor.Utils;

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
            
            AddedMusicBinding.DataSource = AddedMusic;
            NewSoundsBox.DataSource = AddedMusicBinding;
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            SimpleGenreBox.DataSource = Enum.GetValues(typeof(Genre));            
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            Logger.Info($"Commuted to tab {TabControl.SelectedIndex}.{TabControl.Name}");
        }        

        #region Editor

        private void LoadMusicInfo(MusicInfo item)
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

            simpleBoxLoading = true;
            SimpleIdBox.Text = item.Id;
            SimpleTitleBox.Text = WordList.GetBySong(item.Id)?.JapaneseText ?? throw new ArgumentNullException($"Title for '{item.Id}' not found in wordlist");
            SimpleSubtitleBox.Text = WordList.GetBySongSub(item.Id)?.JapaneseText ?? throw new ArgumentNullException($"Subtitle for '{item.Id}' not found in wordlist");
            SimpleDetailBox.Text = WordList.GetBySongDetail(item.Id)?.JapaneseText ?? throw new ArgumentNullException($"Detail for '{item.Id}' not found in wordlist");
            SimpleGenreBox.SelectedItem = MusicOrders.GetByUniqueId(item.UniqueId)?.Genre ?? throw new ArgumentNullException($"Music order entry #{item.UniqueId} could not be found");
            SimpleStarEasyBox.Value = item.StarEasy;
            SimpleStarNormalBox.Value = item.StarNormal;
            SimpleStarHardBox.Value = item.StarHard;
            SimpleStarManiaBox.Value = item.StarMania;
            SimpleStarUraBox.Value = item.StarUra;
            SimpleStarUraBox.Enabled = MusicAttributes.GetByUniqueId(item.UniqueId).CanPlayUra;
            simpleBoxLoading = false;
        }

        private void LoadNewSongData(NewSongData item)
        {
            Logger.Info($"Selection Changed NewSongData: {item}");
            Logger.Info($"Showing properties for NewSongData: {item}");
            MusicInfoGrid.SelectedObject = item?.MusicInfo;
            MusicAttributesGrid.SelectedObject = item?.MusicAttribute;
            MusicOrderGrid.SelectedObject = item?.MusicOrder;
            WordsGrid.SelectedObject = item?.Word;
            WordSubGrid.SelectedObject = item?.WordSub;
            WordDetailGrid.SelectedObject = item?.WordDetail;
            indexChanging = false;

            if (item == null) return;

            simpleBoxLoading = true;
            SimpleIdBox.Text = item.MusicInfo.Id;
            SimpleTitleBox.Text = item.Word.JapaneseText;
            SimpleSubtitleBox.Text = item.WordSub.JapaneseText;
            SimpleDetailBox.Text = item.WordDetail.JapaneseText;
            SimpleGenreBox.SelectedItem = item.MusicOrder.Genre;
            SimpleStarEasyBox.Value = item.MusicInfo.StarEasy;
            SimpleStarNormalBox.Value = item.MusicInfo.StarNormal;
            SimpleStarHardBox.Value = item.MusicInfo.StarHard;
            SimpleStarManiaBox.Value = item.MusicInfo.StarMania;
            SimpleStarUraBox.Value = item.MusicInfo.StarUra;
            SimpleStarUraBox.Enabled = item.MusicAttribute.CanPlayUra;
            simpleBoxLoading = false;
        }

        private bool indexChanging = false;

        private void LoadedMusicBox_SelectedIndexChanged(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            if (indexChanging) return;
            indexChanging = true;
            NewSoundsBox.SelectedItem = null;
            var item = LoadedMusicBox.SelectedItem as MusicInfo;
            Logger.Info($"Selection Changed MusicItem: {item}");
            LoadMusicInfo(item);
            indexChanging = false;
            SoundViewTab.SelectedTab = SoundViewerSimple;
        });

        private void EditorTable_Resize(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            WordsGB.Height = WordSubGB.Height = WordDetailGB.Height = EditorTable.Height / 3;
        });

        private void NewSoundsBox_SelectedIndexChanged(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            if (indexChanging) return;
            indexChanging = true;
            LoadedMusicBox.SelectedItem = null;
            var item = NewSoundsBox.SelectedItem as NewSongData;
            LoadNewSongData(item);
            SoundViewTab.SelectedTab = SoundViewerSimple;
        });

        #endregion        

        private void RemoveNewSong(NewSongData ns)
        {
            AddedMusic.Remove(ns);
            Logger.Info("Refreshing list");
            AddedMusicBinding.ResetBindings(false);
            MusicOrderViewer.RemoveSong(ns.MusicOrder);

            Logger.Info("Removing from wordlist & music_attributes");
            WordList.Items.Remove(ns.Word);
            WordList.Items.Remove(ns.WordDetail);
            WordList.Items.Remove(ns.WordSub);
            MusicAttributes.Items.Remove(ns.MusicAttribute);
        }

        private void RemoveExistingSong(MusicInfo mi)
        {
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
            LoadedMusicBinding.DataSource = MusicInfos.Items.Where(mi => mi.UniqueId != 0).ToList();
            LoadedMusicBinding.ResetBindings(false);

            var sel = LoadedMusicBox.SelectedIndex;

            if (sel >= MusicInfos.Items.Count)
                sel = MusicInfos.Items.Count - 1;

            LoadedMusicBox.SelectedItem = null;
            LoadedMusicBox.SelectedIndex = sel;

            Logger.Info("Removing from music orders");
            MusicOrderViewer.RemoveSong(mo);
        }

        private void RemoveSongButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            Logger.Info("Clicked remove song");
            if (NewSoundsBox.SelectedItem != null)
            {
                Logger.Info("Removing newly added song");
                var ns = NewSoundsBox.SelectedItem as NewSongData;
                RemoveNewSong(ns);
                return;
            }

            if (LoadedMusicBox.SelectedItem != null)
            {
                Logger.Info("Removing existing song");
                var mi = LoadedMusicBox.SelectedItem as MusicInfo;
                RemoveExistingSong(mi);
                return;
            }
        });

        private void SoundViewTab_SelectedIndexChanged(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            if (SoundViewTab.SelectedTab == MusicOrderTab)
            {
                MusicOrderViewer.Invalidate();
                return;
            }

            if (!(SoundViewTab.SelectedTab == SoundViewerExpert || SoundViewTab.SelectedTab == SoundViewerSimple))
                return;

            if (LoadedMusicBox.SelectedItem != null)
            {
                var item = LoadedMusicBox.SelectedItem as MusicInfo;
                Logger.Info($"Tab switched, reloading MusicItem: {item}");
                LoadMusicInfo(item);
                return;
            }

            if (NewSoundsBox.SelectedItem != null)
            {
                var item = NewSoundsBox.SelectedItem as NewSongData;
                Logger.Info($"Tab switched, reloading NewSongData: {item}");
                LoadNewSongData(item);
                return;
            }
        });

        private bool simpleBoxLoading = false;
        private void SimpleBoxChanged(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            if (simpleBoxLoading) return;

            if (LoadedMusicBox.SelectedItem != null)
            {
                var item = LoadedMusicBox.SelectedItem as MusicInfo;

                Logger.Info($"Simple Box changed : {(sender as Control).Name} to value {(sender as Control).Text}");


                WordList.GetBySong(item.Id).JapaneseText = SimpleTitleBox.Text;
                WordList.GetBySongSub(item.Id).JapaneseText = SimpleSubtitleBox.Text;
                WordList.GetBySongDetail(item.Id).JapaneseText = SimpleDetailBox.Text;
                MusicOrders.GetByUniqueId(item.UniqueId).Genre = item.Genre = (Genre)(SimpleGenreBox.SelectedItem ?? Genre.Pop);                
                item.StarEasy = (int)SimpleStarEasyBox.Value;
                item.StarNormal = (int)SimpleStarNormalBox.Value;
                item.StarHard = (int)SimpleStarHardBox.Value;
                item.StarMania = (int)SimpleStarManiaBox.Value;
                item.StarUra = (int)SimpleStarUraBox.Value;
                return;
            }
            else if(NewSoundsBox.SelectedItem!=null)
            {
                var item = NewSoundsBox.SelectedItem as NewSongData;

                Logger.Info($"Simple Box changed : {(sender as Control).Name} to value {(sender as Control).Text}");
                
                item.Word.JapaneseText = SimpleTitleBox.Text;
                item.WordSub.JapaneseText = SimpleSubtitleBox.Text;
                item.WordDetail.JapaneseText = SimpleDetailBox.Text;
                item.MusicOrder.Genre = item.MusicInfo.Genre = (Genre)(SimpleGenreBox.SelectedItem ?? Genre.Pop);
                item.MusicInfo.StarEasy=(int)SimpleStarEasyBox.Value;
                item.MusicInfo.StarNormal=(int)SimpleStarNormalBox.Value;
                item.MusicInfo.StarHard=(int)SimpleStarHardBox.Value;
                item.MusicInfo.StarMania=(int)SimpleStarManiaBox.Value;
                item.MusicInfo.StarUra = (int)SimpleStarUraBox.Value;
                return;
            }
        });

        private void MusicOrderViewer_SongRemoved(Controls.MusicOrderViewer sender, MusicOrder mo) => ExceptionGuard.Run(() =>
        {
            var uniqId = mo.UniqueId;
            var mi = MusicInfos.Items.Where(x => x.UniqueId == uniqId).FirstOrDefault();

            if (mi != null)
            {
                RemoveExistingSong(mi);
                return;
            }

            var ns = AddedMusic.Where(x => x.UniqueId == uniqId).FirstOrDefault();
            if (ns != null)
            {
                RemoveNewSong(ns);
                return;
            }
            throw new InvalidOperationException("Nothing to remove.");
        });

        private void LocateInMusicOrderButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            if (LoadedMusicBox.SelectedItem != null)
            {
                var item = LoadedMusicBox.SelectedItem as MusicInfo;
                var mo = MusicOrders.GetByUniqueId(item.UniqueId);
                if (MusicOrderViewer.Locate(mo))
                {
                    SoundViewTab.SelectedTab = MusicOrderTab;
                }
                return;
            }
            else if (NewSoundsBox.SelectedItem != null)
            {
                var item = NewSoundsBox.SelectedItem as NewSongData;
                if (MusicOrderViewer.Locate(item.MusicOrder))
                {
                    SoundViewTab.SelectedTab = MusicOrderTab;
                }
                return;
            }
        });

        private void MusicOrderViewer_SongDoubleClick(Controls.MusicOrderViewer sender, MusicOrder mo)
        {
            var uid = mo.UniqueId;
            var mi = LoadedMusicBox.Items.Cast<MusicInfo>().Where(_ => _.UniqueId == uid).FirstOrDefault();
            if(mi!=null)
            {
                LoadedMusicBox.SelectedItem = mi;
                return;
            }
            var ns = AddedMusic.Where(_ => _.UniqueId == uid).FirstOrDefault();
            if (ns != null)
            {
                NewSoundsBox.SelectedItem = ns;                
                return;
            }

        }
    }
}