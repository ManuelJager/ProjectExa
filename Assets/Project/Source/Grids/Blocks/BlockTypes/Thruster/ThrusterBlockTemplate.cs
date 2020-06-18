using Exa.Grids.Blocks.Components;
using Exa.UI.Controls;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "Thruster", menuName = "Grids/Blocks/Thruster")]
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

        public override ITooltipComponent[] GetComponents()
        {
            return base.GetComponents()
                .Concat(physicalTemplateComponent.GetComponents())
                .Concat(thrusterTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}