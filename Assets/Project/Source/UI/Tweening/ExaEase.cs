using System;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using UnityEngine;

namespace Exa.UI.Tweening {
    [Serializable]
    public struct ExaEase {
        private static readonly float Tolerance = 0.00001f;
        
        public ExaEaseType easeType;
        public Ease ease;
        public AnimationCurve animationCurve;

        public ExaEase(Ease ease)
            : this(ExaEaseType.Classic, ease, null) { }

        public ExaEase(AnimationCurve curve)
            : this(ExaEaseType.AnimationCurve, Ease.INTERNAL_Zero, curve) { }

        private ExaEase(ExaEaseType easeType, Ease ease, AnimationCurve animationCurve) {
            this.easeType = easeType;
            this.ease = ease;
            this.animationCurve = animationCurve;
        }

        public float Evaluate(float time) {
            var value = easeType switch {
                ExaEaseType.Classic => EaseManager.Evaluate(
                    ease,
                    null,
                    time,
                    1f,
                    0f,
                    0f
                ),
                ExaEaseType.AnimationCurve => animationCurve.Evaluate(time),
                _ => throw new ArgumentOutOfRangeException()
            };

            return value < Tolerance
                ? 0f // Return 0 if value is below tolerance
                : value > 1f - Tolerance
                    ? 1f // Return 1 if value is above 1 - tolerance
                    : value;
        }
    }

    public enum ExaEaseType {
        Classic,
        AnimationCurve
    }

    public static class ExaEaseHelper {
        public static T SetEase<T>(this T tween, ExaEase ease)
            where T : Tween {
            return ease.easeType switch {
                ExaEaseType.Classic => tween.SetEase(ease.ease),
                ExaEaseType.AnimationCurve => tween.SetEase(ease.animationCurve),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}