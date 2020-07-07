using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    [Serializable]
    [CreateAssetMenu(menuName = "Grids/Blocks/Gyroscope")]
    public class GyroscopeBlockTemplate : BlockTemplate<GyroscopeBlock>, IPhysicalBlockTemplateComponent, IGyroscopeBlockTemplateComponent
    {
        [SerializeField] private PhysicalBlockTemplateComponent physicalBlockTemplateComponent;
        [SerializeField] private GyroscopeBlockTemplateComponent gyroscopeBlockTemplateComponent;

        public PhysicalBlockTemplateComponent PhysicalBlockTemplateComponent => physicalBlockTemplateComponent;
        public GyroscopeBlockTemplateComponent GyroscopeBlockTemplateComponent => gyroscopeBlockTemplateComponent;

        protected override void SetValues(GyroscopeBlock block)
        {
            block.PhysicalBlockData = physicalBlockTemplateComponent.Convert();
            block.GyroscopeBlockData = gyroscopeBlockTemplateComponent.Convert();
        }

        protected override ITooltipComponent[] ComponentFactory()
        {
            return base.ComponentFactory()
                .Concat(physicalBlockTemplateComponent.GetComponents())
                .Concat(gyroscopeBlockTemplateComponent.GetComponents())
                .ToArray();
        }
    }
}