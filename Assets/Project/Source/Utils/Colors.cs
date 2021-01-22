using UnityEngine;

namespace Exa.Utils
{
    public static class Colors
    {
        public static Color White => Systems.Colors.white;
        public static Color DimGray => Systems.Colors.dimGray;
        public static Color Onyx => Systems.Colors.onyx;
        public static Color RaisinBlack => Systems.Colors.raisinBlack;
        public static Color Verdigris => Systems.Colors.verdigris;
        public static Color RoseVale => Systems.Colors.roseVale;

        public static Color GetActiveColor(bool active) {
            return active ? Verdigris : RoseVale;
        }
    }
}