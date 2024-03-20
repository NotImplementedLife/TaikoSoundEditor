using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;
using TaikoSoundEditor.Commons.Emit;
using TaikoSoundEditor.Commons.Utils;
using TaikoSoundEditor.Data;
using TaikoSoundEditor.Properties;

namespace TaikoSoundEditor
{
    internal static class Program
    {
        private static void InitTypesJson()
        {
            var wordProps = new List<EntityPropertyInfo>
            {
                new EntityPropertyInfo("Key", typeof(string), defaultValue:"song_...", isReadOnly:true),
                new EntityPropertyInfo("JapaneseText", typeof(string), defaultValue:"text.."),
                new EntityPropertyInfo("JapaneseFontType", typeof(int), defaultValue:0),
            }.Select(EntityPropertyInfoDTO.FromPropertyInfo).ToArray();
            var col = new DynamicTypeCollection(
                new DynamicType("Word", typeof(IWord), wordProps)
                );
            File.WriteAllText("adef.json", JsonSerializer.Serialize(col, new JsonSerializerOptions() { WriteIndented = true }));

        }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            string json = Resources.datatable_def_08_18;
            try
            {
                json = File.ReadAllText(Config.DatatableDefPath);
            }
            catch { }            
            finally
            {
                DatatableTypes.LoadFromJson(json);
                Config.DatatableDefPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datatable_def.json");
                File.WriteAllText(Config.DatatableDefPath, json);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}