using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IBlock, IConvertGameObjectToEntity, IGridMember
    {
        public AnchoredBlueprintBlock anchoredBlueprintBlock;
        public Ship ship;

        public Vector2Int GridAnchor => anchoredBlueprintBlock.gridAnchor;

        public BlueprintBlock BlueprintBlock => anchoredBlueprintBlock.blueprintBlock;

        private void OnDisable()
        {
            ship.blockGrid.Remove(GridAnchor);
        }

        // TODO: Convert gameobject to entity
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            throw new System.NotImplementedException();
        }
    }
}