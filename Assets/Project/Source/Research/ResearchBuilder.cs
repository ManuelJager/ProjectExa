using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;

namespace Exa.Research {
    public class ResearchBuilder {
        private readonly List<Action> clearActions;
        private readonly ResearchStore store;
        private BlockContext context = BlockContext.DefaultGroup;
        private Func<BlockTemplate, bool> filter;

        public ResearchBuilder(ResearchStore store) {
            this.store = store;
            clearActions = new List<Action>();
        }

        public ResearchBuilder Context(BlockContext context) {
            this.context = context;

            return this;
        }

        public ResearchBuilder Add<T>(
            ResearchStep<T>.ApplyValues applyValues,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where T : struct, IBlockComponentValues {

            clearActions.Add(
                item: store.AddModifier(
                    context,
                    modifier: new DynamicBlockComponentModifier(
                        step: new ResearchStep<T>(applyValues, order),
                        affectsTemplate: template => template.GetAnyPartialDataIsOf<T>() && (filter?.Invoke(template) ?? true)
                    )
                )
            );

            return this;
        }

        public ResearchBuilder Add<T>(
            ResearchStep<T>.ApplyValuesOmitInit applyValuesOmitInit,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where T : struct, IBlockComponentValues {
            void ApplyValues(T init, ref T curr) {
                applyValuesOmitInit(ref curr);
            }

            Add<T>(ApplyValues, order);

            return this;
        }

        public ResearchBuilder Filter<TTemplate>()
            where TTemplate : BlockTemplate {
            filter = template => template is TTemplate;

            return this;
        }

        public ResearchBuilder Filter(Func<BlockTemplate, bool> filter) {
            this.filter = filter;
            
            return this;
        }

        public ResearchBuilder ClearFilter() {
            filter = null;

            return this;
        }

        public ResearchBuilder AddFor<TTemplate, TBlockComponentValues>(
            ResearchStep<TBlockComponentValues>.ApplyValuesOmitInit applyValuesOmitInit,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where TTemplate : BlockTemplate
            where TBlockComponentValues : struct, IBlockComponentValues {
            void ApplyValues(TBlockComponentValues init, ref TBlockComponentValues curr) {
                applyValuesOmitInit(ref curr);
            }

            return AddFor<TTemplate, TBlockComponentValues>(ApplyValues, order);
        }

        public ResearchBuilder AddFor<TTemplate, TBlockComponentValues>(
            ResearchStep<TBlockComponentValues>.ApplyValues applyValues,
            ValueModificationOrder order = ValueModificationOrder.Multiplicative
        )
            where TTemplate : BlockTemplate
            where TBlockComponentValues : struct, IBlockComponentValues {
            clearActions.Add(
                store.AddModifier(
                    context,
                    new DynamicBlockComponentModifier(
                        new ResearchStep<TBlockComponentValues>(applyValues, order),
                        template => template is TTemplate
                    )
                )
            );

            return this;
        }

        public void Clear() {
            foreach (var clearAction in clearActions) {
                clearAction();
            }
        }
    }
}