using System.Text.RegularExpressions;


namespace TaikoSoundEditor.Commons.Extensions
{
    internal static class StringExtensions
    {
        public static Match Match(this string s, string regex, string options="")
        {
            var opts = RegexOptions.None;
            if (options.Contains("i")) opts |= RegexOptions.IgnoreCase;


            var r = new Regex(regex);
            if (!r.IsMatch(s)) return null;
            return r.Match(s);
        }

    }
}
