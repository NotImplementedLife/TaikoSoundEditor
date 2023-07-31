using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Utils
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

            MessageBox.Show(e.Message, "An error occured");
            Logger.Error(e);
#endif
            }
        }        
    }
}
