using Exa.Ships;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IBlock, IConvertGameObjectToEntity, IGridMember, IPhysical
    {
        public AnchoredBlueprintBlock anchoredBlueprintBlock;

        [SerializeField] private PhysicalBehaviour physicalBehaviour;
        private Ship ship;

        public Vector2Int GridAnchor => anchoredBlueprintBlock.gridAnchor;

        public BlueprintBlock BlueprintBlock => anchoredBlueprintBlock.blueprintBlock;

        public PhysicalBehaviour PhysicalBehaviour { get => physicalBehaviour; set => physicalBehaviour = value; }

        public virtual Ship Ship
        {
            set
            {
                this.ship = value;
                physicalBehaviour.Ship = value;
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