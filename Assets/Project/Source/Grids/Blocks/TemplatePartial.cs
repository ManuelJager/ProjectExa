using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System;

namespace Exa.Grids.Blocks
{
    public abstract class TemplatePartial<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : struct, IBlockComponentValues
    {
        public abstract T Convert();

        // TODO: Use the given context to apply value modifiers in the conversion step
        public override IBlockComponentValues GetValues(ShipContext blockContext)
        {
            return Convert();
        }

        public override void SetValues(Block block, IBlockComponentValues data)
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
        public abstract IBlockComponentValues GetValues(ShipContext blockContext);

        public abstract void SetValues(Block block, IBlockComponentValues data);

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);
    }
}