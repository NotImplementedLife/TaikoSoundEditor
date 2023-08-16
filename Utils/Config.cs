using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Utils
{
    public static class Config
    {
        private static IniFile CreateIniFile()
        {
            var ini = new IniFile();
            if(!ini.KeyExists("MusicOrderSort"))
            {
                ini.Write(MusicOrderSortProperty, MusicOrderSortValueNone);
            }
            return ini;
        }

        public static readonly IniFile IniFile = CreateIniFile();

        public static void SetMusicOrderSortById() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueId);
        public static void SetMusicOrderNoSort() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueNone);
        public static void SetMusicOrderSortByGenre() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueGenre);

        public static string MusicOrderSort => IniFile.Read(MusicOrderSortProperty);

        public static string MusicOrderSortProperty = "MusicOrderSort";

        public static string MusicOrderSortValueNone = "None";
        public static string MusicOrderSortValueId = "Id";
        public static string MusicOrderSortValueGenre = "Genre";
    }    
}
