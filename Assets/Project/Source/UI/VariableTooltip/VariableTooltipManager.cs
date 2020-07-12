﻿using Exa.Grids.Blocks;
using Exa.Utils;

namespace Exa.UI.Tooltips
{
    public class VariableTooltipManager : MonoSingleton<VariableTooltipManager>
    {
        public TooltipGenerator tooltipGenerator;
        public BlockTemplateTooltip blockTemplateTooltip;
        public BlueprintTypeTooltip blueprintTypeTooltip;
    }
}