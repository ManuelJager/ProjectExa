using Exa.UI.Controls;
using System;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    [Serializable]
    public class BlockTemplateTooltip : VariableTooltipBase<BlockTemplate>
    {
        public override void SetValues(BlockTemplate data)
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltip(data, container);
        }
    }
}