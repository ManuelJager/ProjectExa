using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research
{
    public abstract class BlockComponentModifier<T> : BlockComponentModifier
        where T : struct, IBlockComponentValues
    {
        protected virtual IEnumerable<ResearchStep<T>> GetModifiers() {
            return new[] {
                new ResearchStep<T>(AdditiveStep, ValueModificationOrder.Addition),
                new ResearchStep<T>(MultiplicativeStep, ValueModificationOrder.Multiplicative),
            };
        }

        public override IEnumerable<ResearchStep> GetBaseSteps() {
            return GetModifiers();
        }

        protected virtual void AdditiveStep(T initialData, ref T currentData) { }
        protected virtual void MultiplicativeStep(T initialData, ref T currentData) { }
    }

    public abstract class BlockComponentModifier : ResearchItem
    {
        public abstract IEnumerable<ResearchStep> GetBaseSteps();
    }

    public abstract class ResearchItem : ScriptableObject
    {
        public string Id => this.name;
    }
}