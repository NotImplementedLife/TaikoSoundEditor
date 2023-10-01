using System.Linq;

namespace TaikoSoundEditor.Commons.Extensions
{
    internal static class ObjectExtensions
    {
        public static string ToStringProperties(this object obj)
        {
            return "{" + string.Join("; ", obj.GetType().GetProperties().Select(p => $"{p.Name}=`{p?.GetValue(obj) ?? "(null)"}`")) + "}";
        }

    }
}
