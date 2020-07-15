﻿using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/PowerGenerator")]
    public class PowerGeneratorTemplate : BlockTemplate<PowerGenerator>
    {
        [SerializeField] private PhysicalTemplatePartial physicalTemplatePartial;
        [SerializeField] private PowerGeneratorTemplatePartial powerGeneratorTemplatePartial;

        protected override void SetValues(PowerGenerator block)
        {
            block.physicalBehaviour.data = physicalTemplatePartial.Convert();
            block.powerGeneratorBehaviour.data = powerGeneratorTemplatePartial.Convert();
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplatePartial.GetTooltipComponents())
                .Concat(powerGeneratorTemplatePartial.GetComponents())
                .ToArray();
        }
    }
}