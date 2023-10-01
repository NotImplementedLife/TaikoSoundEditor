using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TaikoSoundEditor.Commons.IO;
using TaikoSoundEditor.Commons.Utils;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor
{
	partial class MainForm
    {
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

		private void TJASelector_PathChanged(object sender, EventArgs args)
		{
			if (TJASelector.Path == null) return;
			if (SongNameBox.Text != "" && SongNameBox.Text != "(6 characters id...)") return;

			var name = Path.GetFileNameWithoutExtension(TJASelector.Path);
			if (name.Length == 6)
				SongNameBox.Text = name;
		}

		private void AddSilenceBox_CheckedChanged(object sender, EventArgs e)
		{
			SilenceBox.Enabled = AddSilenceBox.Checked;
		}

		private void CreateOkButton_Click(object sender, EventArgs e) => ExceptionGuard.Run(() =>
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


			var seconds = AddSilenceBox.Checked ? (int)Math.Ceiling(tja.Headers.Offset + (int)SilenceBox.Value) : 0;
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

			var missingCourses = new int[] { 0, 1, 2, 3 }.Where(i => !tja.Courses.Keys.Contains(i)).ToArray();
			var courseNames = new string[] { "Easy", "Normal", "Hard", "Oni" };

			if (missingCourses.Length > 0)
			{

				var caption = $"There are missing courses in the TJA file for difficulties: {string.Join(", ", missingCourses.Select(i => courseNames[i]))}.\n" +
							  $"Do you want to add a placeholder for the missing courses?";

				if (MessageBox.Show(caption, "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
				{
					foreach (var difficulty in missingCourses)
					{
						text = text.Concat(new string[]
						{
						"",
						$"COURSE:{courseNames[difficulty]}",
						"LEVEL:1",
						"BALLOON:",
						"SCOREINIT:",
						"SCOREDIFF:",
						"#START",
						"1",
						"#END"
						}).ToArray();

						tja.Courses[difficulty] = new TJA.Course(difficulty, new TJA.CourseHeader(), new List<TJA.Measure>
						{
							new TJA.Measure(new int[]{1,1}, new Dictionary<string, bool>(), "1", new List<TJA.MeasureEvent>())
						});
					}
				}
			}


			Logger.Info("Creating temporary tja");
			var newTja = $@".-tmp\{Path.GetFileName(tjaPath)}";
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

			ns.EBin1 = tja_binaries[5];
			ns.HBin1 = tja_binaries[6];
			ns.MBin1 = tja_binaries[7];
			ns.NBin1 = tja_binaries[8];

			ns.EBin2 = tja_binaries[10];
			ns.HBin2 = tja_binaries[11];
			ns.MBin2 = tja_binaries[12];
			ns.NBin2 = tja_binaries[13];


			var selectedMusicInfo = LoadedMusicBox.SelectedItem as IMusicInfo;

			var mi = (selectedMusicInfo ?? (NewSoundsBox.SelectedItem as NewSongData)?.MusicInfo ?? DatatableTypes.CreateMusicInfo()).Clone();
			mi.Id = songName;
			mi.UniqueId = id;
			ns.MusicInfo = mi;

			var mo = DatatableTypes.CreateMusicOrder(Genre.Pop, songName, id);			
			ns.MusicOrder = mo;

			var ma = selectedMusicInfo != null ? MusicAttributes.GetByUniqueId(selectedMusicInfo.UniqueId).Clone() : DatatableTypes.CreateMusicAttribute("", 0);
			ma.Id = songName;
			ma.UniqueId = id;
			ma.New = true;
			ns.MusicAttribute = ma;

			ns.Word = WordList.GetBySong(songName) ?? DatatableTypes.CreateWord($"song_{songName}");
			ns.Word.JapaneseText = tja.Headers.Title;
            ns.WordSub = WordList.GetBySongSub(songName) ?? DatatableTypes.CreateWord($"song_{songName}");
            ns.WordSub.JapaneseText = tja.Headers.Subtitle;
			ns.WordDetail = WordList.GetBySongDetail(songName) ?? DatatableTypes.CreateWord($"song_{songName}", tja.Headers.TitleJa);
            ns.WordDetail.JapaneseText = tja.Headers.TitleJa;

            mi.EasyOnpuNum = tja.Courses[0].NotesCount;
			mi.NormalOnpuNum = tja.Courses[1].NotesCount;
			mi.HardOnpuNum = tja.Courses[2].NotesCount;
			mi.ManiaOnpuNum = tja.Courses[3].NotesCount;

			mi.BranchEasy = tja.Courses[0].HasBranches;
			mi.BranchNormal = tja.Courses[1].HasBranches;
			mi.BranchHard = tja.Courses[2].HasBranches;
			mi.BranchMania = tja.Courses[3].HasBranches;

			mi.StarEasy = tja.Courses[0].Headers.Level;
			mi.StarNormal = tja.Courses[1].Headers.Level;
			mi.StarHard = tja.Courses[2].Headers.Level;
			mi.StarMania = tja.Courses[3].Headers.Level;

			if (tja.Courses.ContainsKey(4))
			{
				FeedbackBox.AppendText("URA course detected\r\n");
				mi.UraOnpuNum = tja.Courses[4].NotesCount;
				mi.StarUra = tja.Courses[4].Headers.Level;
				mi.BranchUra = tja.Courses[4].HasBranches;
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

			if (ma.CanPlayUra)
			{
				ns.XBin = tja_binaries[4];
				ns.XBin1 = tja_binaries[9];
				ns.XBin2 = tja_binaries[14];
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

			Logger.Info("Adding to music orders");

			WordList.Items.Add(ns.Word);
			WordList.Items.Add(ns.WordDetail);
			WordList.Items.Add(ns.WordSub);
			MusicAttributes.Items.Add(ns.MusicAttribute);			

            MusicOrderViewer.AddSong(mo);
        });


		private static void CreateTmpDir()
		{
			Logger.Info($"Creating .-tmp/");
			if (!Directory.Exists(".-tmp"))
				Directory.CreateDirectory(".-tmp");
		}

		private static void WarnWithBox(string message)
		{
			Logger.Warning("Displayed: " + message);
			MessageBox.Show(message);
		}
	}
}
