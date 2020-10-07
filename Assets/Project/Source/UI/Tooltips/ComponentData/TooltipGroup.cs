using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipGroup : ITooltipComponent
    {
        public IEnumerable<ITooltipComponent> Children { get; }
        public int Tabs { get; }
        public int Spacing { get; }

        public TooltipGroup(IEnumerable<ITooltipComponent> children, int tabs = 0, int spacing = 10)
        {
            if (children == null) throw new ArgumentNullException("children", "A collection of children must not be null");

            this.Children = children;
            this.Tabs = tabs;
            this.Spacing = spacing;
        }

        public TooltipComponentView InstantiateComponentView(Transform parent)
        {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipGroup(parent, this);
        }
    }
}