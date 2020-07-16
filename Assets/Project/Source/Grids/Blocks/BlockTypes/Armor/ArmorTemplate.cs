using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Armor")]
    public class ArmorTemplate : BlockTemplate<Armor>
    {
        public PhysicalTemplatePartial physicalTemplatePartial;

        public override void AddContext(Blueprint blueprint)
        {
            physicalTemplatePartial.AddContext(blueprint);
        }

        public override void RemoveContext(Blueprint blueprint)
        {
            physicalTemplatePartial.RemoveContext(blueprint);
        }

        protected override void SetValues(Armor block)
        {
            block.PhysicalBlockBehaviour.data = physicalTemplatePartial.Convert();
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplatePartial.GetTooltipComponents());
        }
    }
}