using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor
{
    internal static class WAV
    {

        public static void ConvertToWav(string sourcePath, string destPath, int seconds_before = 0)
        {
            sourcePath = Path.GetFullPath(sourcePath);
            destPath = Path.GetFullPath(destPath);

            var p = new Process();
            p.StartInfo.FileName = Path.GetFullPath(@"Tools\sox\sox.exe");
            p.StartInfo.ArgumentList.Add(sourcePath);
            p.StartInfo.ArgumentList.Add(destPath);
            /*p.StartInfo.ArgumentList.Add("pad");
            p.StartInfo.ArgumentList.Add(seconds_before.ToString());
            p.StartInfo.ArgumentList.Add("0");*/
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
