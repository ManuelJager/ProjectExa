using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public abstract class TemplatePartial<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : IBlockComponentData
    {
        public abstract T Convert();

        public override void SetValues(Block block)
        {
            (block as IBehaviourMarker<T>).Component.Data = Convert();
        }

        protected S SetupBehaviour<S>(Block block)
            where S : BlockBehaviour<T>
        {
            var behaviour = block.gameObject.AddComponent<S>();
            behaviour.block = block;
            (block as IBehaviourMarker<T>).Component = behaviour;
            return behaviour;
        }

        public override void AddGridTotals(GridTotals totals)
        {
            Convert().AddGridTotals(totals);
        }

        public override void RemoveGridTotals(GridTotals totals)
        {
            Convert().RemoveGridTotals(totals);
        }
    }

    public abstract class TemplatePartialBase : IGridTotalsModifier
    {
        public abstract void SetValues(Block block);

        public abstract BlockBehaviourBase AddSelf(Block block);

        public abstract IEnumerable<ITooltipComponent> GetTooltipComponents();

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);
    }
}