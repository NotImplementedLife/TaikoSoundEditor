using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaikoSoundEditor.Extensions
{
    internal static class StringExtensions
    {
        public static Match Match(this string s, string regex, string options="")
        {
            var opts = RegexOptions.None;
            if (options.Contains('i')) opts |= RegexOptions.IgnoreCase;


            var r = new Regex(regex);
            if (!r.IsMatch(s)) return null;
            return r.Match(s);
        }

    }
}
