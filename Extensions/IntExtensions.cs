namespace TaikoSoundEditor.Extensions
{
    internal static class IntExtensions
    {
        public static int Clamp(this int x, int a, int b) => x <= a ? a : x >= b ? b : x;
    }
}
