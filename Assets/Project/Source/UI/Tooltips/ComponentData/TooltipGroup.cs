﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipGroup : ITooltipComponent
    {
        public IEnumerable<ITooltipComponent> Children { get; }
        public int Tabs { get; }
        public int Spacing { get; }

        public TooltipGroup(IEnumerable<ITooltipComponent> children, int tabs = 0, int spacing = 10) {
            this.Children = children ??
                            throw new ArgumentNullException(nameof(children),
                                "A collection of children must not be null");
            this.Tabs = tabs;
            this.Spacing = spacing;
        }

        public TooltipGroup(int tabs, params ITooltipComponent[] children) {
            this.Tabs = tabs;
            this.Spacing = 10;
            this.Children = children ??
                            throw new ArgumentNullException(nameof(children),
                                "A collection of children must not be null");
        }

        public TooltipComponentView InstantiateComponentView(Transform parent) {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipGroup(parent, this);
        }
    }
}