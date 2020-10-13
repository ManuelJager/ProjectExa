using System;
using DG.Tweening;

namespace Exa.UI.Tweening
{
    public abstract class TweenRef<T>
    {
        private Tween tween;
        private float? defaultDuration;

        public Tween To(T endValue)
        {
            return To(endValue,
                defaultDuration ?? throw new InvalidOperationException("A default duration must be set"));
        }

        public Tween To(T endValue, float time)
        {
            tween?.Kill();
            tween = CreateTween(endValue, time);
            return tween;
        }

        public TweenRef<T> DODefaultDuration(float duration)
        {
            this.defaultDuration = duration;
            return this;
        }

        protected abstract Tween CreateTween(T endValue, float duration);
    }
}