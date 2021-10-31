using System;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids {
    public interface IGridMember : IGridTotalsModifier, IEquatable<IGridMember> {
        Vector2Int GridAnchor { get; }
        BlueprintBlock BlueprintBlock { get; }
    }
}