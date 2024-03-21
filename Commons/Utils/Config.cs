namespace TaikoSoundEditor.Commons.Utils
{
    public static class Config
    {
        private static IniFile CreateIniFile()
        {
            var ini = new IniFile();
            if(!ini.KeyExists("MusicOrderSort")) ini.Write(MusicOrderSortProperty, MusicOrderSortValueNone);
            return ini;
        }

        public static readonly DatatableIO DatatableIO = new DatatableIO();

        public static readonly IniFile IniFile = CreateIniFile();

        public static void SetMusicOrderSortById() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueId);
        public static void SetMusicOrderNoSort() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueNone);
        public static void SetMusicOrderSortByGenre() => IniFile.Write(MusicOrderSortProperty, MusicOrderSortValueGenre);

        public static string DatatableDefPath
        {
            get => IniFile.Read(DatatableDefPathProperty);
            set => IniFile.Write(DatatableDefPathProperty, value);
        }

        public static string DatatablesPath
        {
            get => IniFile.Read("DatatablePath");
            set => IniFile.Write("DatatablePath", value);
        }

        public static string FumenKey
        {
            get => IniFile.Read("FumenKey");
            set => IniFile.Write("FumenKey", value);
        }

        public static string DatatableKey
        {
            get => IniFile.Read("DatatableKey");
            set => IniFile.Write("DatatableKey", value);
        }

        public static bool UseEncryption
        {
            get => System.Convert.ToBoolean(IniFile.Read("UseEncryption") == "True" ? true : false);
            set => IniFile.Write("UseEncryption", value.ToString());
        }

        public static string MusicOrderSort => IniFile.Read(MusicOrderSortProperty);

        public static string MusicOrderSortProperty = "MusicOrderSort";
        public static string DatatableDefPathProperty = "DatatableDef";

        public static string MusicOrderSortValueNone = "None";
        public static string MusicOrderSortValueId = "Id";
        public static string MusicOrderSortValueGenre = "Genre";        
    }    
}
