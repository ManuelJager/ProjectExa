using Exa.Math;
using UnityEngine;

namespace Exa.Utils
{
    public static class Texture2DExtensions
    {
        public static Texture2D DrawCircle(this Texture2D tex, Color color, Vector2 centre, float radius, bool clearBackground = false) {
            foreach (var point in MathUtils.EnumerateVectors(tex.width, tex.height)) {
                if ((centre - point).magnitude < radius) {
                    tex.SetPixel(point.x, point.y, color);
                } else if (clearBackground) {
                    tex.SetPixel(point.x, point.y, Color.clear);
                }
            }

            tex.Apply();

            return tex;
        }

        public static Sprite CreateSprite(this Texture2D tex, float pixelsPerUnit = 32) {
            var rect = new Rect(0, 0, tex.width, tex.height);
            return Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }

        public static Texture2D SetDefaults(this Texture2D tex) {
            tex.filterMode = FilterMode.Point;
            return tex;
        }
    }
}