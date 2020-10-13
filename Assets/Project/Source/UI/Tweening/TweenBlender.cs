using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using Unity.Burst;

namespace Exa.UI.Tweening
{
    public class TweenBlender<TValue, TTarget>
        where TValue : struct
        where TTarget : CustomTargetTween<TValue>, new()
    {
        private Action<TValue> setter;
        private TValue defaultValue;
        private Dictionary<int, Tween> tweens = new Dictionary<int, Tween>();
        private Dictionary<int, TValue> values = new Dictionary<int, TValue>();

        protected Func<TValue, TValue, TValue> aggregator = null;

        public TweenBlender(TValue defaultValue, Action<TValue> setter, Func<TValue, TValue, TValue> aggregator)
        {
            this.defaultValue = defaultValue;
            this.setter = setter;
            this.aggregator = aggregator;
        }

        public Tween To(int id, TValue endValue, float time)
        {
            return To(id, values.ContainsKey(id) ? values[id] : defaultValue, endValue, time);
        }

        public Tween To(int id, TValue startValue, TValue endValue, float duration)
        {
            if (tweens.ContainsKey(id))
            {
                tweens[id].Kill();
            }

            values[id] = startValue;

            var tween = new TTarget()
                .DOGetter(() => values[id])
                .DOSetter(x => values[id] = x)
                .To(endValue, duration);

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

        protected virtual TValue BlendValues(TValue value, IEnumerable<TValue> blenders)
        {
            if (aggregator == null)
                throw new InvalidOperationException("Cannot blend values without a given aggregator function");

            return blenders.Aggregate(value, aggregator);
        }
    }

    public class FloatTweenBlender : TweenBlender<float, FloatTween>
    {
        public FloatTweenBlender(float defaultValue, Action<float> setter, Func<float, float, float> aggregator) 
            : base(defaultValue, setter, aggregator)
        {
        }
    }
}