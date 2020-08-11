using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipTitle : ITooltipComponent
    {
        public string Text { get; private set; }

        public TooltipTitle(string text)
        {
            Text = text;
        }

        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipTitle(this, parent);            
        }
    }
}