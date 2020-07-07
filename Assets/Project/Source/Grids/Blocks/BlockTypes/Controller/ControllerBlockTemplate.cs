using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Controller")]
    public class ControllerBlockTemplate : BlockTemplate<ControllerBlock>, IPhysicalBlockTemplateComponent
    {
        public PhysicalBlockTemplateComponent physicalTemplateComponent;

        public PhysicalBlockTemplateComponent PhysicalBlockTemplateComponent => physicalTemplateComponent;

        protected override void SetValues(ControllerBlock block)
        {
            block.PhysicalBlockData = physicalTemplateComponent.Convert();
        }

        protected override ITooltipComponent[] ComponentFactory()
        {
            return base.ComponentFactory()
                .Concat(physicalTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}