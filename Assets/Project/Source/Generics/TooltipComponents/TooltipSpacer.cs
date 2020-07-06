using Exa.UI.Controls;
using UnityEngine;

namespace Exa.Generics
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public GameObject InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipSpacer(this, parent);
        }
    }
}