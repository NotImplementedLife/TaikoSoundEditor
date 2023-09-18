using System.Diagnostics;
using System.Globalization;
using System.Text;
using TaikoSoundEditor.Data;
using TaikoSoundEditor.Utils;

namespace TaikoSoundEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {                               
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            SSL.LoadKeys();                       


            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}