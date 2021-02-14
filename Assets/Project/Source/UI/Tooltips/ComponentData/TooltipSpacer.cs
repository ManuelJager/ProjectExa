using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.Tooltips.tooltipGenerator.GenerateTooltipSpacer(parent, this);
        }
    }
}