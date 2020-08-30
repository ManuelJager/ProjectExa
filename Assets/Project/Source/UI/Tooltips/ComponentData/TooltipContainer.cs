using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipContainer : ITooltipComponent
    {
        private IEnumerable<ITooltipComponent> children;
        private int tabs;

        public TooltipContainer(IEnumerable<ITooltipComponent> children, int tabs = 0)
        {
            this.children = children;
            this.tabs = tabs;
        }

        public Component InstantiateComponentView(Transform parent)
        {
            var containerView = Systems.UI.tooltips.tooltipGenerator.GenerateTooltipContainer(parent);
            containerView.SetTabs(tabs);

            foreach (var child in children)
            {
                child.InstantiateComponentView(containerView.container);
            }

            return containerView;
        }
    }
}