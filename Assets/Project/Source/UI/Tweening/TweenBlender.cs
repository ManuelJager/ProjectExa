using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;

namespace Exa.UI.Tweening
{
    public abstract class TweenBlender<T>
        where T : struct
    {
        private Action<T> setter;
        private T defaultValue;
        private Dictionary<int, Tween> tweens = new Dictionary<int, Tween>();
        private Dictionary<int, T> values = new Dictionary<int, T>();

        protected TweenBlender(T defaultValue, Action<T> setter)
        {
            this.defaultValue = defaultValue;
            this.setter = setter;
        }

        public Tween To(int id, T endValue, float time)
        {
            return To(id, values.ContainsKey(id) ? values[id] : defaultValue, endValue, time);
        }

        public Tween To(int id, T startValue, T endValue, float time)
        {
            if (tweens.ContainsKey(id))
            {
                tweens[id].Kill();
            }

            values[id] = startValue;
            var tween = CreateTween(() => values[id], x => values[id] = x, endValue, time);
            tweens[id] = tween;
            return tween;
        }

        public void Update()
        {
            var blenders = values.Values;
            var blendedValue = BlendValues(defaultValue, blenders);
            setter(blendedValue);
        }

        public void Kill()
        {
            foreach (var tween in tweens.Values)
            {
                tween.Kill();
            }
        }

        protected abstract T BlendValues(T value, IEnumerable<T> blenders);
        protected abstract Tween CreateTween(DOGetter<T> getter, DOSetter<T> setter, T endValue, float time);
    }

    public class FloatTweenBlender : TweenBlender<float>
    {
        public FloatTweenBlender(float defaultValue, Action<float> setter)
            : base(defaultValue, setter)
        {
        }

        protected override float BlendValues(float value, IEnumerable<float> blenders)
        {
            foreach (var blender in blenders)
            {
                value *= blender;
            }
            return value;
        }

        protected override Tween CreateTween(DOGetter<float> getter, DOSetter<float> setter, float endValue, float time)
        {
            return DOTween.To(getter, setter, endValue, time);
        }
    }
}