using System.Collections.Generic;
using System.Linq;

namespace TaikoSoundEditor.Commons.Utils
{
    public static class ProcessArgs
    {
        public static string GetString(params string[] args)
        {
            return string.Join(" ", args.Select(_ => _.Contains(" ") ? $"\"{_}\"" : _)); // does NOT support "something \" like this"
        }
    }
}
