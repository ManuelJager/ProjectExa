using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks
{
    public abstract class TemplatePartial<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : struct, IBlockComponentValues
    {
        public abstract T ToBaseComponentValues();

        public T ToContextfulComponentValues(BlockContext blockContext) {
            return Systems.Research.ApplyModifiers(blockContext, ToBaseComponentValues());
        }

        public override IBlockComponentValues GetContextfulValues(BlockContext blockContext) {
            return ToContextfulComponentValues(blockContext);
        }

        public override void SetValues(Block block, IBlockComponentValues data) {
            var partial = GetMarker(block);
            partial.Component.Data = (T) data;
        }

        // NOTE: Cannot use context-ful values as the given grid totals have no context
        public override void AddGridTotals(GridTotals totals) {
            ToBaseComponentValues().AddGridTotals(totals);
        }

        // NOTE: Cannot use context-ful values as the given grid totals have no context
        public override void RemoveGridTotals(GridTotals totals) {
            ToBaseComponentValues().RemoveGridTotals(totals);
        }

        public override Type GetTargetType() {
            return typeof(T);
        }

        private IBehaviourMarker<T> GetMarker(Block block) {
            var partial = block as IBehaviourMarker<T>;

            if (partial == null) {
                var partialString = typeof(IBehaviourMarker<T>).ToGenericString();
                var blockString = block.GetType().ToGenericString();
                throw new Exception($"Partial {partialString} is not supported on block: {blockString}");
            }

            if (partial.Component == null) {
                throw new Exception($"Block behaviour for {typeof(T).Name} is null");
            }

            return partial;
        }
    }

    public abstract class TemplatePartialBase : IGridTotalsModifier
    {
        public abstract IBlockComponentValues GetContextfulValues(BlockContext blockContext);

        public abstract void SetValues(Block block, IBlockComponentValues data);

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);

        public abstract Type GetTargetType();
    }
}