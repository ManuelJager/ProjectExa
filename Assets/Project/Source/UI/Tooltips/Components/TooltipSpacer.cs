﻿using UnityEngine;

namespace Exa.UI.Tooltips
{
    public struct TooltipSpacer : ITooltipComponent
    {
        public TooltipComponentBundle InstantiateComponentView(Transform parent)
        {
            return Systems.UI.tooltips.tooltipGenerator.GenerateTooltipSpacer(this, parent);
        }
    }
}