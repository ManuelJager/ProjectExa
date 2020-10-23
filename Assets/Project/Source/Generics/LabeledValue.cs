using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Generics
{
    public struct LabeledValue<T> : ILabeledValue<T>, IEquatable<LabeledValue<T>>, ITooltipComponent
    {
        public string Label { get; set; }
        public T Value { get; set; }

        public LabeledValue(string label, T value) {
            Label = label;
            Value = value;
        }

        public bool Equals(LabeledValue<T> other) {
            return
                Label.Equals(other.Label) &&
                Value.Equals(other.Value);
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipProperty(parent, this);
        }
    }

    public interface ILabeledValue<out T>
    {
        string Label { get; }
        T Value { get; }
    }
}