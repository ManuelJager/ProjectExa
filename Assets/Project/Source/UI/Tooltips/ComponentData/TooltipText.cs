using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipText : ITooltipComponent
    {
        public string Text { get; }

        public TooltipText(string value) {
            this.Text = value;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.Tooltips.tooltipGenerator.GenerateTooltipText(parent, this);
        }
    }
}