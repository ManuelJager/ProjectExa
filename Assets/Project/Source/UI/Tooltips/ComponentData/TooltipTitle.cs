﻿using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipTitle : ITooltipComponent
    {
        private bool animated;

        public string Text { get; private set; }

        public TooltipTitle(string text, bool animated = true)
        {
            this.animated = animated;

            Text = text;
        }

        public Component InstantiateComponentView(Transform parent)
        {
            var titleView = Systems.UI.tooltips.tooltipGenerator.GenerateTooltipTitle(parent);
            titleView.Reflect(this);

            if (animated)
            {
                titleView.AddAnimator();
            }

            return titleView;
        }
    }
}