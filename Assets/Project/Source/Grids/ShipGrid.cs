using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class ShipGrid : MonoBehaviour
    {
        public void Import(Blueprint blueprint)
        {
            foreach (var anchoredBlueprintBlock in blueprint.Blocks.AnchoredBlueprintBlocks)
            {
                var block = anchoredBlueprintBlock.CreateBehaviourInGrid(transform, BlockPrefabType.userGroup);
                block.shipGrid = this;
                block.gameObject.SetActive(true);
            }
        }
    }
}