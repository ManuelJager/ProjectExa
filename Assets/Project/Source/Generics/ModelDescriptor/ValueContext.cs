using Exa.UI.Controls;
using UnityEngine;

namespace Exa.Generics
{
    public struct ValueContext : ITooltipComponent
    {
        public string value;
        public string name;

        public GameObject InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipProperty(this, parent);
        }
    }
}