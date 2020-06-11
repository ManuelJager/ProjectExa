﻿using Exa.Grids.Blocks;
using Exa.Utils;

namespace Exa.UI.Controls
{
    public class VariableTooltipManager : MonoBehaviourInstance<VariableTooltipManager>
    {
        public TooltipGenerator tooltipGenerator;
        public BlockTemplateTooltip blockTemplateTooltip;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}