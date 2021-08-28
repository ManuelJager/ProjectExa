using System;
using UnityEngine;

namespace Exa.UI.Tooltips {
    public abstract class TooltipComponentView<T> : TooltipComponentView
        where T : ITooltipComponent {
        protected abstract void Refresh(T value);

        public override void Refresh(ITooltipComponent value) {
            try {
                Refresh(value: (T) value);
            } catch (InvalidCastException) {
                throw new ArgumentException(message: $"{value.GetType()} cannot be converted to {typeof(T)}");
            }
        }
    }

    public abstract class TooltipComponentView : MonoBehaviour {
        public abstract void Refresh(ITooltipComponent value);
    }
}