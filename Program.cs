using System.Diagnostics;
using System.Globalization;
using System.Text;

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




            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}