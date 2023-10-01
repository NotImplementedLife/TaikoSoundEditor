using System;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace TaikoSoundEditor.Commons.Utils
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

        public static Color[] GenreColors =
            new Color[] 
            {
                Color.FromArgb(73, 213, 235), // pop
                Color.FromArgb(254, 144, 210), // anime
                Color.FromArgb(253, 192, 0), // kids                
                Color.FromArgb(203, 207, 222), // vocaloid
                Color.FromArgb(204, 138, 235), // game music
                Color.FromArgb(255, 112, 40), // Namco Original
                Color.FromArgb(255, 255, 255), // ??
                Color.FromArgb(10, 204, 42), // variety
                Color.FromArgb(222, 213, 35), // classic                
            }
            .Concat(Colors.Where(c => 0.5 <= c.GetBrightness() && c.GetBrightness() < 0.7 && c.GetSaturation() > 0.5))
            .ToArray();    
    }
}
