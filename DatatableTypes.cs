using System;
using TaikoSoundEditor.Commons.Emit;
using TaikoSoundEditor.Commons.IO;
using TaikoSoundEditor.Data;

namespace TaikoSoundEditor
{
    internal static class DatatableTypes
    {
        public static Type Word { get; private set; }
        public static Type MusicOrder { get; private set; }
        public static Type MusicAttribute { get; private set; }
        public static Type MusicInfo { get; private set; }


        public static IWord CreateWord(string key, string japaneseText = "")
        {
            var w = Activator.CreateInstance(Word) as IWord;
            w.JapaneseText = japaneseText;
            w.Key = key;
            return w;
        }

        public static IMusicOrder CreateMusicOrder(Genre genre, string id, int uniqId, int closeDispType = 0)
        {
            var nmo = Activator.CreateInstance(MusicOrder) as IMusicOrder;
            nmo.Genre = genre;
            nmo.Id = id;
            nmo.UniqueId = uniqId;
            nmo.CloseDispType = closeDispType;
            return nmo;
        }

        public static IMusicAttribute CreateMusicAttribute(string id, int uniqId, bool isNew = true)
        {
            var ma = Activator.CreateInstance(MusicAttribute) as IMusicAttribute;
            ma.Id = id;
            ma.UniqueId = uniqId;
            ma.New = isNew;
            return ma;
        }

        public static IMusicInfo CreateMusicInfo(string id="abcdef", int uniqId=0)
        {
            var mi = Activator.CreateInstance(MusicInfo) as IMusicInfo;
            mi.Id = id;
            mi.UniqueId = uniqId;
            return mi;
        }

        public static T Clone<T>(this T item)
        {
            var type = item.GetType();
            var props = type.GetProperties();
            var clone = (T)Activator.CreateInstance(type);
            foreach (var p in props)
                p.SetValue(clone, p.GetValue(item));
            return clone;
        }                 

        public static void LoadFromJson(string json)
        {
            var types = DatatableEntityTypeBuilder.LoadTypes(Json.Deserialize<DynamicTypeCollection>(json));
            Word = types["Word"];
            MusicOrder = types["MusicOrder"];
            MusicAttribute = types["MusicAttribute"];
            MusicInfo = types["MusicInfo"];
        }        


        public static string ToString(this IMusicInfo mi) => $"{mi.UniqueId}. {mi.Id}";
    }
}
