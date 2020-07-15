using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Armor")]
    public class ArmorTemplate : BlockTemplate<Armor>
    {
        public PhysicalTemplatePartial physicalTemplateComponent;

        protected override void SetValues(Armor block)
        {
            block.PhysicalBlockBehaviour.data = physicalTemplateComponent.Convert();
        }

        protected override ITooltipComponent[] TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplateComponent.GetTooltipComponents())
                .ToArray();
        }
    }
}