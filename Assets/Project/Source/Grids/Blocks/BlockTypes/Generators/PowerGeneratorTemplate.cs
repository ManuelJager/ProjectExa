using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/PowerGenerator")]
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

        protected override ITooltipComponent[] ComponentFactory()
        {
            return base.ComponentFactory()
                .Concat(physicalBlockTemplateComponent.GetComponents())
                .Concat(powerGeneratorBlockTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}