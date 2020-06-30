using Exa.UI.Controls;
using UnityEngine;

namespace Exa.Generics
{
    public struct NamedValue<T> : ITooltipComponent, INamedValue<T>
    {
        public string Name;
        public T Value { get; set; }

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