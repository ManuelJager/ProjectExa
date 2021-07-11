using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Utils;

namespace Exa.Grids.Blocks {
    public abstract class TemplatePartialBase<T> : TemplatePartialBase, ITemplatePartial<T>
        where T : struct, IBlockComponentValues {
        public abstract T ToBaseComponentValues();

        public T ToContextfulComponentValues(BlockContext blockContext) {
            return S.Research.ApplyModifiers(blockContext, Template, ToBaseComponentValues());
        }

        public override IBlockComponentValues GetContextlessValues() {
            return ToBaseComponentValues();
        }

        public override IBlockComponentValues GetContextfulValues(BlockContext blockContext) {
            return ToContextfulComponentValues(blockContext);
        }

        public override void SetValues(Block block, IBlockComponentValues data) {
            var partial = GetMarker(block);
            partial.Component.Data = (T) data;
        }

        public override void AddGridTotals(GridTotals totals) {
            S.Blocks.Values.GetValues<T>(totals.GetInjectedContext(), Template).AddGridTotals(totals);
        }

        public override void RemoveGridTotals(GridTotals totals) {
            S.Blocks.Values.GetValues<T>(totals.GetInjectedContext(), Template).RemoveGridTotals(totals);
        }

        public override Type GetDataType() {
            return typeof(T);
        }

        private static IBehaviourMarker<T> GetMarker(Block block) {
            if (!(block is IBehaviourMarker<T> partial)) {
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

    public abstract class TemplatePartialBase : IGridTotalsModifier, ITemplatePartial {
        public BlockTemplate Template { get; internal set; }

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);

        public abstract IBlockComponentValues GetContextlessValues();

        public abstract IBlockComponentValues GetContextfulValues(BlockContext blockContext);

        public abstract void SetValues(Block block, IBlockComponentValues data);

        public abstract Type GetDataType();
    }
}