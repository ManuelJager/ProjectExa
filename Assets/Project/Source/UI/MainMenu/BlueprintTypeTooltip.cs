using Exa.Grids.Blueprints;
using Exa.UI.Controls;
using UnityEngine;

namespace Exa.UI
{
    public class BlueprintTypeTooltip : VariableTooltipBase<BlueprintType>
    {
        public override void SetValues(BlueprintType data)
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltip(data, container);
        }
    }
}