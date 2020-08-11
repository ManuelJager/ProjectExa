using Exa.AI;
using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class VariableTooltipManager : MonoBehaviour
    {
        public TooltipGenerator tooltipGenerator;
        public BlockTemplateTooltip blockTemplateTooltip;
        public BlueprintTypeTooltip blueprintTypeTooltip;
        public ShipAIDebugTooltip shipAIDebugTooltip;
    }
}