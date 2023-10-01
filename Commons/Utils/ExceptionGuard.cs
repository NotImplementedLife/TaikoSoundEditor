using System;
using System.Windows.Forms;

namespace TaikoSoundEditor.Commons.Utils
{
    internal static class ExceptionGuard
    {
        public static void Run(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
#if DEBUG
                Logger.Error(ex);
                if (MessageBox.Show(ex.Message + "\n\nThrow?", "An error occured", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    throw;            
#else

            MessageBox.Show(ex.Message, "An error occured");
            Logger.Error(ex);
#endif
            }
        }        
    }
}
