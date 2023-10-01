using System;
using System.Linq;
namespace TaikoSoundEditor.Commons.Utils
{
    internal static class Types
    {
        public static object GetDefaultValue(this Type type)
            => type.IsValueType ? Activator.CreateInstance(type) : null;

        public static Type GetTypeByName(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                                .Where(a => !a.IsDynamic)
                                .SelectMany(a => a.GetTypes())
                                .FirstOrDefault(t => t.Name == name || t.FullName == name);
        }
    }
}
