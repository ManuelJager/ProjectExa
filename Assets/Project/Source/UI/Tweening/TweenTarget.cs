using System;
using System.Linq.Expressions;
using DG.Tweening;
using DG.Tweening.Core;

namespace Exa.UI.Tweening
{
    public abstract class TweenTarget<T> : TweenRef<T>
    {
        protected DOGetter<T> getter;
        protected DOSetter<T> setter;

        public TweenTarget<T> DOGetter(DOGetter<T> getter)
        {
            this.getter = getter;
            return this;
        }

        public TweenTarget<T> DOSetter(DOSetter<T> setter)
        {
            this.setter = setter;
            return this;
        }
    }

    public class FloatTweenTarget : TweenTarget<float>
    {
        protected override Tween CreateTween(float endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }

    public class IntTweenTarget : TweenTarget<int>
    {
        protected override Tween CreateTween(int endValue, float duration)
        {
            return DOTween.To(getter, setter, endValue, duration);
        }
    }
}