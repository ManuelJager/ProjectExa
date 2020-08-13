using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public interface IGridMember : IGridTotalsModifier
    {
        Vector2Int GridAnchor { get; }
        BlueprintBlock BlueprintBlock { get; }
    }
}