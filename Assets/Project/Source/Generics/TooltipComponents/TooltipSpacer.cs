using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Generics
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipSpacer(this, parent);
        }
    }
}