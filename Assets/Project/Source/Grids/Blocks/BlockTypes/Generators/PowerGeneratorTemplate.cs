using Exa.Grids.Blocks.Components;
using Exa.UI.Controls;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(fileName = "PowerGenerator", menuName = "Grids/Blocks/PowerGenerator")]
    public class PowerGeneratorTemplate : BlockTemplate<PowerGenerator>, IPhysicalBlockTemplateComponent, IPowerGeneratorBlockTemplateComponent
    {
        [SerializeField] private PhysicalBlockTemplateComponent physicalBlockTemplateComponent;
        [SerializeField] private PowerGeneratorBlockTemplateComponent powerGeneratorBlockTemplateComponent;

        public PhysicalBlockTemplateComponent PhysicalBlockTemplateComponent => physicalBlockTemplateComponent;

        public PowerGeneratorBlockTemplateComponent PowerGeneratorBlockTemplateComponent => powerGeneratorBlockTemplateComponent;

        protected override void SetValues(PowerGenerator block)
        {
            block.PhysicalBlockData = physicalBlockTemplateComponent.Convert();
            block.PowerGeneratorBlockData = powerGeneratorBlockTemplateComponent.Convert();
        }

        public override ITooltipComponent[] GetComponents()
        {
            return base.GetComponents()
                .Concat(physicalBlockTemplateComponent.GetComponents())
                .Concat(powerGeneratorBlockTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}