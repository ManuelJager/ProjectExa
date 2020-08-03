using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public interface IGridMember
    {
        Vector2Int GridAnchor { get; }
        BlueprintBlock BlueprintBlock { get; }
    }
}