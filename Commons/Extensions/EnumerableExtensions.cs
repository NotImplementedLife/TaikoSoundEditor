using System;
using System.Collections.Generic;

namespace TaikoSoundEditor.Commons.Extensions
{
    internal static class EnumerableExtensions
    {
        /*public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, T item)
        {
            yield return item;
            foreach (var i in items) yield return i;
        }*/

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> a)
        {
            foreach (var item in items) a(item);
        }
    }
}
