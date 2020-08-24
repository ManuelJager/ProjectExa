using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
using System.Collections.Generic;

namespace Exa.Grids.Blocks
{
    public abstract class TemplatePartial<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : struct, IBlockComponentData
    {
        public abstract T Convert();

        // TODO: Use the given context to apply value modifiers in the conversion step
        public override IBlockComponentData SetValues(Block block, BlockContext blockContext)
        {
            var partial = GetMarker(block);
            var values = Convert();
            partial.Component.Data = values;
            return values;
        }

        public override void SetValues(Block block, IBlockComponentData data)
        {
            var partial = GetMarker(block);
            partial.Component.Data = (T)data;
        }

        public override void AddGridTotals(GridTotals totals)
        {
            Convert().AddGridTotals(totals);
        }

        public override void RemoveGridTotals(GridTotals totals)
        {
            Convert().RemoveGridTotals(totals);
        }

        private IBehaviourMarker<T> GetMarker(Block block)
        {
            var partial = block as IBehaviourMarker<T>;

            if (partial == null)
            {
                throw new Exception($"Partial {typeof(IBehaviourMarker<T>)} is not supported on block: {block}");
            }

            if (partial.Component == null)
            {
                throw new Exception($"Block behaviour for {typeof(T).Name} is null");
            }

            return partial;
        }
    }

    public abstract class TemplatePartialBase : IGridTotalsModifier
    {
        public abstract IBlockComponentData SetValues(Block block, BlockContext blockContext);
        public abstract void SetValues(Block block, IBlockComponentData data);

        public abstract IEnumerable<ITooltipComponent> GetTooltipComponents();

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);
    }
}