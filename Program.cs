using System.Diagnostics;
using System.Globalization;

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
            try
            {
                int x = 0;
                int y = 2 / x;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }


            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}