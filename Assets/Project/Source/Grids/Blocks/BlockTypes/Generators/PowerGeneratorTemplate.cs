using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/PowerGenerator")]
    public class PowerGeneratorTemplate : PhysicalBlockTemplate<PowerGenerator>
    {
        [SerializeField] private PowerGeneratorTemplatePartial powerGeneratorTemplatePartial;

        public PowerGeneratorTemplatePartial PowerGeneratorTemplatePartial { get => powerGeneratorTemplatePartial; set => powerGeneratorTemplatePartial = value; }

        public override void SetValues(PowerGenerator block)
        {
            base.SetValues(block);
            block.PowerGeneratorBehaviour.data = powerGeneratorTemplatePartial.Convert();
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(powerGeneratorTemplatePartial.GetComponents());
        }
    }
}