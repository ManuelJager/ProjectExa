using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Gyroscope")]
    public class GyroscopeTemplate : BlockTemplate<Gyroscope>
    {
        [SerializeField] private PhysicalTemplatePartial physicalTemplatePartial;
        [SerializeField] private GyroscopeTemplatePartial gyroscopeTemplatePartial;

        protected override void SetValues(Gyroscope block)
        {
            block.PhysicalBehaviour.data = physicalTemplatePartial.Convert();
            block.GyroscopeBehaviour.data = gyroscopeTemplatePartial.Convert();
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplatePartial.GetTooltipComponents())
                .Concat(gyroscopeTemplatePartial.GetComponents());
        }
    }
}