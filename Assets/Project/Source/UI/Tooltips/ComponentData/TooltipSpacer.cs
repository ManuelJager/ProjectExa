using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public Component InstantiateComponentView(Transform parent)
        {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipSpacer(parent);
        }
    }
}