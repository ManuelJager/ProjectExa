using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Generics
{
    public struct NamedWrapper<T> : ITooltipComponent, INamedValue<T>, IEquatable<NamedWrapper<T>>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public NamedWrapper(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public bool Equals(NamedWrapper<T> other)
        {
            return
                Name.Equals(other.Name) &&
                Value.Equals(other.Value);
        }

        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipProperty<T>(parent);
        }
    }

    public interface INamedValue<out T>
    {
        T Value { get; }
    }
}