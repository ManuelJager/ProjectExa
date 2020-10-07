using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public TooltipComponentView InstantiateComponentView(Transform parent)
        {
            return Systems.Ui.tooltips.tooltipGenerator.GenerateTooltipSpacer(parent, this);
        }
    }
}