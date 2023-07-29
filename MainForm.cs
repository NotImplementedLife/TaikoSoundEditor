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
            Logger.Info($"Commuted to tab {TabControl.SelectedIndex}");
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
            SimpleTitleBox.Text = WordList.GetBySong(item.Id).JapaneseText;
            SimpleSubtitleBox.Text = WordList.GetBySongSub(item.Id).JapaneseText;
            SimpleDetailBox.Text = WordList.GetBySongDetail(item.Id).JapaneseText;
            SimpleGenreBox.SelectedItem = MusicOrders.GetByUniqueId(item.UniqueId).Genre;
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

        private void LoadedMusicBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (indexChanging) return;
            indexChanging = true;
            NewSoundsBox.SelectedItem = null;
            var item = LoadedMusicBox.SelectedItem as MusicInfo;
            Logger.Info($"Selection Changed MusicItem: {item}");
            LoadMusicInfo(item);
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
            LoadNewSongData(item);
        }

        #endregion        

        private void RemoveSongButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
        {
            Logger.Info("Clicked remove song");
            if (NewSoundsBox.SelectedItem != null)
            {
                Logger.Info("Removing newly added song");
                var ns = NewSoundsBox.SelectedItem as NewSongData;
                AddedMusic.Remove(ns);
                Logger.Info("Refreshing list");
                AddedMusicBinding.ResetBindings(false);
                MusicOrderViewer.RemoveSong(ns.MusicOrder);

                Logger.Info("Removing from wordlist & music_attributes");
                WordList.Items.Remove(ns.Word);
                WordList.Items.Remove(ns.WordDetail);
                WordList.Items.Remove(ns.WordSub);
                MusicAttributes.Items.Remove(ns.MusicAttribute);

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
                LoadedMusicBinding.DataSource = MusicInfos.Items.Where(mi => mi.UniqueId != 0).ToList();
                LoadedMusicBinding.ResetBindings(false);

                var sel = LoadedMusicBox.SelectedIndex;

                if (sel >= MusicInfos.Items.Count)
                    sel = MusicInfos.Items.Count - 1;

                LoadedMusicBox.SelectedItem = null;
                LoadedMusicBox.SelectedIndex = sel;

                Logger.Info("Removing from music orders");
                MusicOrderViewer.RemoveSong(mo);                

                return;
            }
        });

        private void SoundViewTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(SoundViewTab.SelectedTab==MusicOrderTab)
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

            if(NewSoundsBox.SelectedItem!=null)
            {
                var item = NewSoundsBox.SelectedItem as NewSongData;
                Logger.Info($"Tab switched, reloading NewSongData: {item}");
                LoadNewSongData(item);
                return;
            }                       
        }

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
                MusicOrders.GetByUniqueId(item.UniqueId).Genre = (Genre)(SimpleGenreBox.SelectedItem ?? Genre.Pop);
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
                item.MusicOrder.Genre = (Genre)(SimpleGenreBox.SelectedItem ?? Genre.Pop);
                item.MusicInfo.StarEasy=(int)SimpleStarEasyBox.Value;
                item.MusicInfo.StarNormal=(int)SimpleStarNormalBox.Value;
                item.MusicInfo.StarHard=(int)SimpleStarHardBox.Value;
                item.MusicInfo.StarMania=(int)SimpleStarManiaBox.Value;
                item.MusicInfo.StarUra = (int)SimpleStarUraBox.Value;
                return;
            }
        });
    }
}