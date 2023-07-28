using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TaikoSoundEditor.Utils
{
    internal static class Constants
    {
        static Random ColorRand = new Random(1236);

        public static Color[] Colors = typeof(Color)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .Where(p => p.PropertyType == typeof(Color)).Select(p => (Color)p.GetValue(null))
            .Where(c => c != Color.Transparent)
            .OrderBy(_ => ColorRand.Next())
            .ToArray();

        public static Color[] GenreColors = Colors.Where(c => 0.5 <= c.GetBrightness() && c.GetBrightness() < 0.7 && c.GetSaturation() > 0.5).ToArray();
    }
}
