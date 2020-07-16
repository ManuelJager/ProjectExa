using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class ShipGrid : MonoBehaviour
    {
        internal void Import(Blueprint blueprint)
        {
            foreach (var block in blueprint.Blocks.AnchoredBlueprintBlocks)
            {
                block.CreateBehaviourInGrid(transform, BlockPrefabType.userGroup);
            }
        }
    }
}