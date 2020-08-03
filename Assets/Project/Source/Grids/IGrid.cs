using Exa.Generics;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids
{
    public interface IGrid<T>
        where T : IGridMember
    {
        LazyCache<Vector2Int> Size { get; }
        LazyCache<Vector2> CentreOfMass { get; }
        List<T> GridMembers { get; }
        Dictionary<Vector2Int, T> OccupiedTiles { get; }
        Dictionary<T, List<T>> NeighbourDict { get; }
    }
}