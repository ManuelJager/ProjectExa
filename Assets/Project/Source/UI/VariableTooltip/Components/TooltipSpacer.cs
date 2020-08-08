using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return Systems.MainUI.variableTooltipManager.tooltipGenerator.GenerateTooltipSpacer(this, parent);
        }
    }
}