using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipText : ITooltipComponent
    {
        private string value;

        public TooltipText(string value)
        {
            this.value = value;
        }

        public Component InstantiateComponentView(Transform parent)
        {
            var textView = Systems.UI.tooltips.tooltipGenerator.GenerateTooltipText(parent);
            textView.SetText(value);
            return textView;
        }
    }
}