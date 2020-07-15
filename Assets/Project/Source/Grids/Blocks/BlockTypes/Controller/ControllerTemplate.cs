using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Controller")]
    public class ControllerTemplate : BlockTemplate<Controller>
    {
        public PhysicalTemplatePartial physicalTemplateComponent;

        protected override void SetValues(Controller block)
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