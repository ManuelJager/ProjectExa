using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;

namespace Exa.Research {
    public abstract class BlockComponentModifier<T> : BlockComponentModifier
        where T : struct, IBlockComponentValues {
        protected virtual IEnumerable<ResearchStep<T>> GetModifiers() {
            return new[] {
                new ResearchStep<T>(AdditiveStep, ValueModificationOrder.Addition),
                new ResearchStep<T>(MultiplicativeStep, ValueModificationOrder.Multiplicative)
            };
        }

        public override IEnumerable<ResearchStep> GetResearchSteps() {
            return GetModifiers();
        }

        public override Type GetTargetType() {
            return typeof(T);
        }

        protected virtual void AdditiveStep(T initialData, ref T currentData) { }

        protected virtual void MultiplicativeStep(T initialData, ref T currentData) { }
    }

    public abstract class BlockComponentModifier : ResearchItem, IBlockComponentModifier {
        public virtual bool AffectsTemplate(BlockTemplate template) {
            bool Filter(TemplatePartialBase partial) {
                return partial.GetTargetType() == GetTargetType();
            }

            // This makes sure the modifier only affects block templates which contain partials with the same target type
            // This can be overriden to provide custom target behaviour
            return template.GetTemplatePartials().Any(Filter);
        }

        public abstract IEnumerable<ResearchStep> GetResearchSteps();

        public override void EnableOn(BlockContext filter) {
            Systems.Research.AddModifier(filter, this);
        }

        public override void DisableOn(BlockContext filter) {
            Systems.Research.RemoveModifier(filter, this);
        }

        public abstract Type GetTargetType();
    }
}