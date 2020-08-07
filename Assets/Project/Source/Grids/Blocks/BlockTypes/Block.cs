using Exa.Grids.Blueprints;
using Exa.Grids.Ships;
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

        private Ship ship;

        public Vector2Int GridAnchor => anchoredBlueprintBlock.gridAnchor;

        public BlueprintBlock BlueprintBlock => anchoredBlueprintBlock.blueprintBlock;

        public virtual Ship Ship
        {
            set
            {
                this.ship = value;
            }
        }

        protected virtual void OnDisable()
        {
            ship.blockGrid.Remove(GridAnchor);
            Ship = null;
        }

        // TODO: Convert gameobject to entity
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            throw new System.NotImplementedException();
        }
    }
}