using System;
using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;

namespace Exa.Research
{
    public class ResearchBuilder
    {
        private BlockContext context = BlockContext.DefaultGroup;
        private ResearchStore store;

        private List<Action> clearActions;

        public ResearchBuilder(ResearchStore store) {
            this.store = store;
            this.clearActions = new List<Action>();
        }

        public ResearchBuilder Context(BlockContext context) {
            this.context = context;
            return this;
        }

        public ResearchBuilder Add<T>(ResearchStep<T>.ApplyValues applyValues,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative)
            where T : struct, IBlockComponentValues {
            clearActions.Add(store.AddModifier(context, applyValues, order));
            return this;
        }
        public ResearchBuilder Add<T>(ResearchStep<T>.ApplyValuesOmitInit applyValuesOmitInit,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative)
            where T : struct, IBlockComponentValues {
            void ApplyValues(T init, ref T curr) => applyValuesOmitInit(ref curr);
            
            clearActions.Add(store.AddModifier(context, (ResearchStep<T>.ApplyValues) ApplyValues, order));
            return this;
        }
        public void Clear() {
            foreach (var clearAction in clearActions)
                clearAction();
        }
    }
}