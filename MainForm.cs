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

            AddedMusicBinding = new BindingSource();
            AddedMusicBinding.DataSource = AddedMusic;
            NewSoundsBox.DataSource = AddedMusicBinding;
            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
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
                LoadedMusicBinding.DataSource = MusicInfos.Items.Where(mi => mi.UniqueId != 0).ToList();
                LoadedMusicBinding.ResetBindings(false);

                var sel = LoadedMusicBox.SelectedIndex;

                if (sel >= MusicInfos.Items.Count)
                    sel = MusicInfos.Items.Count - 1;

                LoadedMusicBox.SelectedItem = null;
                LoadedMusicBox.SelectedIndex = sel;
                return;
            }
        });        
    }
}