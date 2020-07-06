using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Thruster")]
    public class ThrusterBlockTemplate : BlockTemplate<ThrusterBlock>, IPhysicalBlockTemplateComponent, IThrusterBlockTemplateComponent
    {
        public PhysicalBlockTemplateComponent physicalTemplateComponent;
        public ThrusterBlockTemplateComponent thrusterTemplateComponent;

        public PhysicalBlockTemplateComponent PhysicalBlockTemplateComponent => physicalTemplateComponent;
        public ThrusterBlockTemplateComponent ThrusterBlockTemplateComponent => thrusterTemplateComponent;

        protected override void SetValues(ThrusterBlock block)
        {
            block.PhysicalBlockData = physicalTemplateComponent.Convert();
            block.ThrusterBlockData = thrusterTemplateComponent.Convert();
        }

        protected override ITooltipComponent[] ComponentFactory()
        {
            return base.ComponentFactory()
                .Concat(physicalTemplateComponent.GetComponents())
                .Concat(thrusterTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}