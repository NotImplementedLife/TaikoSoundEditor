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
                Error(ex);
            }
        }

        public static void Error(Exception e)
        {
            MessageBox.Show(e.Message, "An error occured");
            Logger.Error(e);
        }
    }
}
