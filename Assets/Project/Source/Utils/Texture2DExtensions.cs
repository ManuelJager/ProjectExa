using System;
using System.Linq;
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

        public static Texture2D DrawSuperSampledCone(this Texture2D tex, ConeArgs coneArgs, SuperSamplingArgs<float> @override = null) {
            var useEasing = coneArgs.easingFunc != null;
            var args = new SuperSamplingArgs<float> {
                sampleSize = 4,
                applier = (pixel, value) => {
                    tex.SetPixel(pixel, coneArgs.color.SetAlpha(value));
                },
                samplesAverageFactory = values => values.Average(),
                sampler = (point, localPoint) => ConeSampler(localPoint, coneArgs, useEasing)
            };
            return tex.CallbackCircularSuperSampledDraw(coneArgs.centre, @override?.Override(args) ?? args);
        }

        public static float ConeSampler(Vector2 localPoint, ConeArgs args, bool ease) {
            var magnitude = localPoint.magnitude;
            var halfArc = args.arc / 2f;
            return magnitude < args.radius
                ? localPoint.GetAngle().Between(180 - halfArc, 180 + halfArc)
                    ? ease 
                        ? args.easingFunc(magnitude / args.radius)
                        : 1f
                    : 0f
                : 0f;
        }

        public static Texture2D CallbackCircularSuperSampledDraw<T>(this Texture2D tex, Vector2 centre, SuperSamplingArgs<T> samplingArgs) {
            foreach (var pixel in tex.EnumeratePixels()) {
                var samples = GetSamples(pixel, samplingArgs.sampleSize)
                    .Select(point => samplingArgs.sampler(point, centre - point))
                    .ToList();
                samplingArgs.applier(pixel, samplingArgs.samplesAverageFactory(samples));
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

        public static IEnumerable<Vector2> GetSamples(Vector2Int origin, int count) {
            var size = 1f / count;
            var halfSize = size / 2f;
            for (var x = 0; x < count; x++) {
                for (var y = 0; y < count; y++) {
                    yield return new Vector2 {
                        x = origin.x + halfSize + x * size,
                        y = origin.y + halfSize + y * size
                    };
                }
            }
        }

        public static Color GetPixel(this Texture2D tex, Vector2Int point) {
            return tex.GetPixel(point.x, point.y);
        }

        public static void SetPixel(this Texture2D tex, Vector2Int point, Color color) {
            tex.SetPixel(point.x, point.y, color);
        }

        public struct ConeArgs
        {
            public Color color;
            public Vector2 centre;
            public float radius;
            public float arc;
            public Func<float, float> easingFunc;
        }

        public class SuperSamplingArgs<TSample>
        {
            public delegate TSample Sampler(Vector2 point, Vector2 localPoint);
            public delegate TSample SamplesAverageFactory(IEnumerable<TSample> values);
            public delegate void Applier(Vector2Int pixel, TSample averagedValue);

            public int sampleSize;
            public Sampler sampler;
            public SamplesAverageFactory samplesAverageFactory;
            public Applier applier;

            public SuperSamplingArgs<TSample> Override(SuperSamplingArgs<TSample> original) {
                if (sampleSize != 0) {
                    original.sampleSize = sampleSize;
                }

                if (sampler != null) {
                    original.sampler = sampler;
                }

                if (samplesAverageFactory != null) {
                    original.samplesAverageFactory = samplesAverageFactory;
                }

                if (applier != null) {
                    original.applier = applier;
                }

                return original;
            }
        }
    }
}