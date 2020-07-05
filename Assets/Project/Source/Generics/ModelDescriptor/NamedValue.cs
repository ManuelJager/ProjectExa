using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Generics
{
    public struct NamedValue<T> : ITooltipComponent, INamedValue<T>, IEquatable<NamedValue<T>>
    {
        public string Name;
        public T Value { get; set; }

        public bool Equals(NamedValue<T> other)
        {
            return
                Name.Equals(other.Name) &&
                Value.Equals(other.Value);
        }

        public GameObject InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipProperty(this, parent);
        }
    }

    public interface INamedValue<out T>
    {
        T Value { get; }
    }
}