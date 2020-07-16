using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PhysicalBlockTemplate<T> : BlockTemplate<T>
        where T : PhysicalBlock
    {
        [SerializeField] private PhysicalTemplatePartial physicalTemplatePartial;

        public PhysicalTemplatePartial PhysicalTemplatePartial { get => physicalTemplatePartial; set => physicalTemplatePartial = value; }

        public override void SetValues(T block)
        {
            block.PhysicalBlockBehaviour.data = physicalTemplatePartial.Convert();
        }

        protected override T BuildOnGameObject(GameObject gameObject)
        {
            var instance = base.BuildOnGameObject(gameObject);
            instance.PhysicalBlockBehaviour = AddBlockBehaviour<PhysicalBehaviour>(instance);
            return instance;
        }

        protected override IEnumerable<ITooltipComponent> TooltipComponentFactory()
        {
            return base.TooltipComponentFactory()
                .Concat(physicalTemplatePartial.GetTooltipComponents());
        }
    }
}