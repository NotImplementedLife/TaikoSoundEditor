using System;

namespace TaikoSoundEditor.Commons.Utils
{
    internal static class Number
    {
        public static int ParseInt(string value)
        {
            try
            {
                return int.Parse(value);
            }
            catch(FormatException ex)
            {
                throw new FormatException($"{ex.Message} : '{value ?? "<null>"}' to int", ex);
            }
        }

        public static float ParseFloat(string value)
        {
            try
            {
                return float.Parse(value);
            }
            catch (FormatException ex)
            {
                throw new FormatException($"{ex.Message} : {value} to float", ex);
            }
        }
    }
}
