using UnityEngine;

namespace Exa.Utils {
    public static class Colors {
        public static Color White {
            get => Systems.Colors.white;
        }

        public static Color DimGray {
            get => Systems.Colors.dimGray;
        }

        public static Color Onyx {
            get => Systems.Colors.onyx;
        }

        public static Color RaisinBlack {
            get => Systems.Colors.raisinBlack;
        }

        public static Color Verdigris {
            get => Systems.Colors.verdigris;
        }

        public static Color RoseVale {
            get => Systems.Colors.roseVale;
        }

        public static Color GetActiveColor(bool active) {
            return active ? Verdigris : RoseVale;
        }
    }
}