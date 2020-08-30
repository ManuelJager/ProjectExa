using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Generics
{
    public struct LabeledValue<T> : ITooltipComponent, ILabeledValue<T>, IEquatable<LabeledValue<T>>
    {
        public string Label { get; set; }
        public T Value { get; set; }

        public LabeledValue(string label, T value)
        {
            Label = label;
            Value = value;
        }

        public bool Equals(LabeledValue<T> other)
        {
            return
                Label.Equals(other.Label) &&
                Value.Equals(other.Value);
        }

        public Component InstantiateComponentView(Transform parent)
        {
            var propertyView = Systems.UI.tooltips.tooltipGenerator.GenerateTooltipProperty(parent);
            propertyView.Reflect(this);
            return propertyView;
        }
    }

    public interface ILabeledValue<out T>
    {
        T Value { get; }
    }
}