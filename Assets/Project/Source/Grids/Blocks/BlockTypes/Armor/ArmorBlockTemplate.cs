using Exa.Grids.Blocks.Components;
using Exa.UI.Controls;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Armor", menuName = "Grids/Blocks/Armor")]
    public class ArmorBlockTemplate : BlockTemplate<ArmorBlock>
    {
        public PhysicalBlockTemplateComponent physicalTemplateComponent;

        protected override void SetValues(ArmorBlock block)
        {
            block.PhysicalBlockData = physicalTemplateComponent.Convert();
        }

        public override ITooltipComponent[] GetComponents()
        {
            return base.GetComponents()
                .Concat(physicalTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}