using Exa.UI.Tooltips;
using System.Collections.Generic;

namespace Exa.Grids.Blocks.Components
{
    public interface IBlockComponentValues : IGridTotalsModifier
    {
        IEnumerable<ITooltipComponent> GetTooltipComponents();
    }
}