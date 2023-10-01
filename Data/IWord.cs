namespace TaikoSoundEditor.Data
{
    public interface IWord
    {
        string Key { get; set; }
        string JapaneseText { get; set; }
        int JapaneseFontType { get; set; }
    }
}
