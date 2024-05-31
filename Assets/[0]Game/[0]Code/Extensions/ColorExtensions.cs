using UnityEngine;

namespace Game
{
    public static class ColorExtensions
    {
        public static Color SetA(this Color color, float alpha)
        {
            color.a = alpha;
            return color;
        }
    }
}