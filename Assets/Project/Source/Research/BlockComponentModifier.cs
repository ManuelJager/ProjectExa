using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Research
{
    public abstract class BlockComponentModifier<T> : BlockComponentModifier
        where T : struct, IBlockComponentValues
    {
        public abstract IEnumerable<ResearchStep<T>> GetModifiers();

        public override void AddSelf(BlockContext context) {
            var steps = new List<ResearchStep>(GetModifiers());
            Systems.Research.GetContext(context).AddSteps(Id, steps);
        }

        public override void RemoveSelf(BlockContext context) {
            Systems.Research.GetContext(context).RemoveSteps(Id);
        }
    }

    public abstract class BlockComponentModifier : ResearchItem
    {

    }

    public abstract class ResearchItem : ScriptableObject
    {
        public string Id => this.name;

        public abstract void AddSelf(BlockContext context);
        public abstract void RemoveSelf(BlockContext context);
    }
}