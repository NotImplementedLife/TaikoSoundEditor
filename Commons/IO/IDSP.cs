using System;
using System.Diagnostics;
using System.IO;
using TaikoSoundEditor.Commons.Utils;

namespace TaikoSoundEditor.Commons.IO
{
    internal static class IDSP
    {
        public static void WavToIdsp(string source, string dest)
        {
            Logger.Info("Converting WAV to IDSP");            

            source = Path.GetFullPath(source);
            dest = Path.GetFullPath(dest);

            Logger.Info($"source = {source}");
            Logger.Info($"dest = {dest}");

            var p = new Process();
            p.StartInfo.FileName = Path.GetFullPath(@"Tools\VGAudio\VGAudioCli.exe");
            p.StartInfo.Arguments = ProcessArgs.GetString(source, dest);            
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
