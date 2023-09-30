using System.Diagnostics;
using TaikoSoundEditor.Data;
using TaikoSoundEditor.Utils;

namespace TaikoSoundEditor
{
    partial class MainForm
    {
        private void ExportDatatable(string path)
        {
            Logger.Info($"Exporting Datatable to '{path}'");
            var mi = new MusicInfos();
            mi.Items.AddRange(MusicInfos.Items);
            mi.Items.AddRange(AddedMusic.Select(_ => _.MusicInfo));

            var ma = new MusicAttributes();
            ma.Items.AddRange(MusicAttributes.Items);
            //ma.Items.AddRange(AddedMusic.Select(_ => _.MusicAttribute));

            var mo = new MusicOrders();
            mo.Items.AddRange(MusicOrderViewer.SongCards.Select(_ => _.MusicOrder));

            var wl = new WordList();
            wl.Items.AddRange(WordList.Items);           

            Config.DatatableIO.Serialize(Path.Combine(path, "musicinfo.bin"), mi, indented: !DatatableSpaces.Checked);
            Config.DatatableIO.Serialize(Path.Combine(path, "music_attribute.bin"), ma, fixBools: true);
            Config.DatatableIO.Serialize(Path.Combine(path, "music_order.bin"), mo);
            Config.DatatableIO.Serialize(Path.Combine(path, "wordlist.bin"), wl, indented: true);            
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

                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_e_1.bin"), ns.EBin1);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_n_1.bin"), ns.NBin1);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_h_1.bin"), ns.HBin1);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_m_1.bin"), ns.MBin1);

                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_e_2.bin"), ns.EBin2);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_n_2.bin"), ns.NBin2);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_h_2.bin"), ns.HBin2);
                File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_m_2.bin"), ns.MBin2);

                if (ns.MusicAttribute.CanPlayUra)
                {
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x.bin"), ns.XBin);
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x_1.bin"), ns.XBin1);
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_x_2.bin"), ns.XBin2);
                }
            }
        }


        private void ExportDatatableButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
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

        private void ExportSoundFoldersButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
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

        private void ExportSoundBanksButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
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

        private void ExportAllButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
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

            var nupath = Path.Combine(path, "sound");
            if (!Directory.Exists(nupath)) Directory.CreateDirectory(nupath);

            var sbpath = Path.Combine(path, "fumen");
            if (!Directory.Exists(sbpath)) Directory.CreateDirectory(sbpath);

            ExportDatatable(dtpath);
            ExportSoundBinaries(sbpath);
            ExportNusBanks(nupath);
            MessageBox.Show("Done");
            if (ExportOpenOnFinished.Checked)
                Process.Start($"explorer.exe", path);
        });      
    }
}
