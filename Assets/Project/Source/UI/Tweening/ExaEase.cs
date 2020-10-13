using System;
using DG.Tweening;
using UnityEngine;

namespace Exa.UI.Tweening
{
    [Serializable]
    public struct ExaEase
    {
        public ExaEaseType easeType;
        public Ease ease;
        public AnimationCurve animationCurve;
    }

    public enum ExaEaseType
    {
        Classic,
        AnimationCurve
    }

    public static class ExaEaseHelper
    {
        public static T SetEase<T>(this T tween, ExaEase ease)
            where T : Tween
        {
            switch (ease.easeType)
            {
                case ExaEaseType.Classic:
                    return tween.SetEase(ease.ease);
                case ExaEaseType.AnimationCurve:
                    return tween.SetEase(ease.animationCurve);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}