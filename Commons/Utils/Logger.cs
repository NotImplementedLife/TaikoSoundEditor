using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;

namespace TaikoSoundEditor.Commons.Utils
{
    internal static class Logger
    {
        static string LogFile;

        static Logger()
        {
            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");
            LogFile = $"tse_{DateTime.Now:yyyy-MM-dd_hh-mm-ss}.log";
            LogFile = Path.Combine("logs", LogFile);

            Info("Session started");
        }

        private static void Write(string message)
        {
            try
            {
                File.AppendAllText(LogFile, message);
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to write log file:\n" + e.Message);
            }
        }

        private static void Write(string type,  string message)
        {
            Write($"[{type} {DateTime.Now:yyyy-MM-dd_hh-mm-ss}]{message}\n");            
        }

        public static void Info(string message)=> Write("INFO", message);
        public static void Warning(string message)=> Write("WARN", message);
        public static void Error(string message)=> Write("ERROR", message);
        public static void Error(Exception e)
        {
            Error($"Exception raised:\n{e.Message}\n{e.StackTrace}\nFrom {e.Source}\nData:\n");
            foreach(DictionaryEntry kv in e.Data)
            {
                Write($"{kv.Key} = {kv.Value}");
            }
            Write("\n\n");
            if (e.GetBaseException() != null && e.GetBaseException() != e)
            {
                Error(e.GetBaseException());
                Write("Base exception:");
            }
            if(e.InnerException!=null)
            {
                Write("Inner exception:");
                Error(e.InnerException);
            }                         
        }        


    }
}
