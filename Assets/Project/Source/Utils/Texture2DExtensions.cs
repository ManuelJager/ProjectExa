using System;
using System.Collections.Generic;
using Exa.Math;
using UnityEngine;
using MathUtils = Exa.Math.MathUtils;

namespace Exa.Utils
{
    public static class Texture2DExtensions
    {
        public static Texture2D DrawCone(this Texture2D tex, Color color, Vector2 centre, float radius, float arc) {
            var halfArc = arc / 2f;
            return tex.CallbackCircularDraw(centre, radius, (localPoint, point)=> {
                if (localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)) {
                    tex.SetPixel(point, color);
                }
            });
        }

        public static Texture2D DrawFadingCone(this Texture2D tex, Color color, Vector2 centre, float radius,
            float arc, Func<float, float> easingFunc) {
            var halfArc = arc / 2f;
            return tex.CallbackCircularDraw(centre, radius, (localPoint, point)=> {
                if (localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)) {
                    tex.SetPixel(point, color.SetAlpha(color.a * easingFunc(localPoint.magnitude / radius)));
                }
            });
        }

        public static Texture2D CallbackCircularDraw(this Texture2D tex, Vector2 centre, float radius, Action<Vector2, Vector2Int> pixelCallback) {
            foreach (var point in tex.EnumeratePixels()) {
                var localPoint = centre - point;
                if (localPoint.magnitude < radius) {
                    pixelCallback(localPoint, point);
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
            tex.EnumeratePixels().ForEach(pixel => tex.SetPixel(pixel, Color.clear));
            return tex;
        }

        public static IEnumerable<Vector2Int> EnumeratePixels(this Texture2D tex) {
            return MathUtils.EnumerateVectors(tex.width, tex.height);
        }

        public static void SetPixel(this Texture2D tex, Vector2Int point, Color color) {
            tex.SetPixel(point.x, point.y, color);
        }
    }
}