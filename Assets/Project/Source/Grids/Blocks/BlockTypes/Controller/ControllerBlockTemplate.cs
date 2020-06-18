using Exa.Grids.Blocks.Components;
using Exa.UI.Controls;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Controller", menuName = "Grids/Blocks/Controller")]
    public class ControllerBlockTemplate : BlockTemplate<ControllerBlock>, IPhysicalBlockTemplateComponent
    {
        public PhysicalBlockTemplateComponent physicalTemplateComponent;

        public PhysicalBlockTemplateComponent PhysicalBlockTemplateComponent => physicalTemplateComponent;

        protected override void SetValues(ControllerBlock block)
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