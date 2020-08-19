using Exa.Debugging;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.UI.Tooltips;
using System;
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
            var partial = block as IBehaviourMarker<T>;
            if (partial == null)
            {
                throw new Exception($"Partial {typeof(IBehaviourMarker<T>)} is not supported on block: {block}");
            }

            partial.Component.Data = Convert();
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

        public abstract IEnumerable<ITooltipComponent> GetTooltipComponents();

        public abstract void AddGridTotals(GridTotals totals);

        public abstract void RemoveGridTotals(GridTotals totals);
    }
}