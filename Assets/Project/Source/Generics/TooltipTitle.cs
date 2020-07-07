using Exa.UI.Tooltips;
using UnityEngine;

namespace Exa.Generics
{
    public struct TooltipTitle : ITooltipComponent
    {
        public string Text { get; private set; }

        public TooltipTitle(string text)
        {
            Text = text;
        }

        public GameObject InstantiateComponentView(Transform parent)
        {
            return VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltipTitle(this, parent);
        }
    }
}