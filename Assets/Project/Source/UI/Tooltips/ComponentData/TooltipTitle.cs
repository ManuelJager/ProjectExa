using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipTitle : ITooltipComponent
    {
        private readonly bool _animated;

        public string Text { get; private set; }

        public TooltipTitle(string text, bool animated = true)
        {
            this._animated = animated;

            Text = text;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent)
        {
            var titleView = Systems.Ui.tooltips.tooltipGenerator.GenerateTooltipTitle(parent, this);

            if (_animated)
            {
                titleView.AddAnimator();
            }

            return titleView;
        }
    }
}