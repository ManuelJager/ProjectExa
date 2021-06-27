using Exa.Grids.Blocks;
using Exa.Ships;
using UnityEngine;

namespace Exa.Grids {
    public interface IGridInstance {
        BlockGrid BlockGrid { get; }
        Rigidbody2D Rigidbody2D { get; }
        Transform Transform { get; }
        BlockContext BlockContext { get; }
        GridInstanceConfiguration Configuration { get; }
    }
}