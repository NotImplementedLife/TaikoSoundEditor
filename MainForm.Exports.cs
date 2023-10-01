using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Windows.Forms;
using TaikoSoundEditor.Collections;
using TaikoSoundEditor.Commons;
using TaikoSoundEditor.Commons.IO;
using TaikoSoundEditor.Commons.Utils;
using TaikoSoundEditor.Data;

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

            Config.DatatableIO.DynamicSerialize(Path.Combine(path, "musicinfo.bin"), mi.Cast(DatatableTypes.MusicInfo) , indented: !DatatableSpaces.Checked);
            Config.DatatableIO.DynamicSerialize(Path.Combine(path, "music_attribute.bin"), ma.Cast(DatatableTypes.MusicAttribute), fixBools: true);
            Config.DatatableIO.DynamicSerialize(Path.Combine(path, "music_order.bin"), mo.Cast(DatatableTypes.MusicOrder));
            Config.DatatableIO.DynamicSerialize(Path.Combine(path, "wordlist.bin"), wl.Cast(DatatableTypes.Word), indented: true);
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

                void Save(string suffix, byte[] bytes)
                {
                    if (UseEncryptionBox.Checked)
                        bytes = SSL.EncryptFumen(bytes);
                    File.WriteAllBytes(Path.Combine(sdir, $"{ns.Id}_{suffix}.bin"), bytes);
                }

                Save("e", ns.EBin);
                Save("n", ns.NBin);
                Save("h", ns.HBin);
                Save("m", ns.MBin);

                Save("e_1", ns.EBin1);
                Save("n_1", ns.NBin1);
                Save("h_1", ns.HBin1);
                Save("m_1", ns.MBin1);

                Save("e_2", ns.EBin2);
                Save("n_2", ns.NBin2);
                Save("h_2", ns.HBin2);
                Save("m_2", ns.MBin2);               

                if (ns.MusicAttribute.CanPlayUra)
                {
                    Save("x", ns.XBin);
                    Save("x_1", ns.XBin1);
                    Save("x_2", ns.XBin2);                    
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

        private static string PickPath()
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
