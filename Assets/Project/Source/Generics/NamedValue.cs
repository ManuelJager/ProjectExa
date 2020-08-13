using Exa.UI.Tooltips;
using System;
using UnityEngine;

namespace Exa.Generics
{
    public struct NamedValue<T> : ITooltipComponent, INamedValue<T>, IEquatable<NamedValue<T>>
    {
        public string Name { get; set; }
        public T Value { get; set; }

        public NamedValue(string name, T value)
        {
            Name = name;
            Value = value;
        }

        public bool Equals(NamedValue<T> other)
        {
            return
                Name.Equals(other.Name) &&
                Value.Equals(other.Value);
        }

        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipProperty<T>(parent);
        }
    }

    public interface INamedValue<out T>
    {
        T Value { get; }
    }
}