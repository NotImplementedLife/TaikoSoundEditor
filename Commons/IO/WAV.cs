using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.IO
{
    internal static class WAV
    {

        public static void ConvertToWav(string sourcePath, string destPath, int seconds_before)
        {
            sourcePath = Path.GetFullPath(sourcePath);
            destPath = Path.GetFullPath(destPath);

            var p = new Process();

            p.StartInfo.FileName = Path.GetFullPath(@"Tools\sox\sox.exe");
            p.StartInfo.Arguments = ProcessArgs.GetString(sourcePath, destPath, "pad", seconds_before.ToString(), "0");            
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            p.WaitForExit();
            int exitCode = p.ExitCode;
            string stdout = p.StandardOutput.ReadToEnd();
            string stderr = p.StandardError.ReadToEnd();

            if (exitCode != 0)
                throw new Exception($"Process sox failed with exit code {exitCode}:\n" + stderr);
        }

    }
}
