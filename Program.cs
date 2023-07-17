using System.Diagnostics;

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
            /*var tja = new TJA(File.ReadAllLines($@"C:\Users\NotImpLife\Desktop\nus\EXAMPLE .TJA\hoshis\hoshis.tja"));
            File.WriteAllText("tja.txt", tja.ToString());

            for (int i = 0; i < 4; i++)
            {
                var c = tja.Courses[i].Converted;
                Debug.WriteLine(i+" "+c.Notes.Length);
                //Debug.WriteLine(c.Events.Length);
            }            

            return;*/
            //File.WriteAllText("tja.txt", tja.ToString());

            
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}