using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor
{
    internal static class IDSP
    {
        public static void WavToIdsp(string source, string dest)
        {
            source = Path.GetFullPath(source);
            dest = Path.GetFullPath(dest);

            Debug.WriteLine(source);
            Debug.WriteLine(dest);

            var p = new Process();
            p.StartInfo.FileName = Path.GetFullPath(@"Tools\VGAudio\VGAudioCli.exe");
            p.StartInfo.ArgumentList.Add(source);
            p.StartInfo.ArgumentList.Add(dest);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = false;

            p.Start();
            p.WaitForExit();
            int exitCode = p.ExitCode;            
            string stderr = p.StandardError.ReadToEnd();

            if (exitCode != 0)
                throw new Exception($"Process VGAudioCli failed with exit code {exitCode}:\n" + stderr);
        }
    }
}
