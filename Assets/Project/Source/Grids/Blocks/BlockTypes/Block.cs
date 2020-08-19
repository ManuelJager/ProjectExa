using Exa.Ships;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;

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

        BlockBehaviour<PhysicalData> IBehaviourMarker<PhysicalData>.Component
        {
            get => physicalBehaviour; 
            set => physicalBehaviour = value as PhysicalBehaviour; 
        }

        public Ship Ship
        {
            protected get => ship;
            set
            {
                this.ship = value;

                if (ship != null)
                {
                    OnAdd();
                }
                else if (!Systems.IsQuitting)
                {
                    OnRemove();
                }

                foreach (var behaviour in GetBehaviours())
                {
                    behaviour.Ship = value;
                }
            }
        }

        private void OnDisable()
        {
            ship.blockGrid.Remove(GridAnchor);
            Ship = null;
        }

        public void AddGridTotals(GridTotals totals)
        {
            foreach (var behaviour in GetBehaviours())
            {
                behaviour.BlockComponentData.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals)
        {
            foreach (var behaviour in GetBehaviours())
            {
                behaviour.BlockComponentData.RemoveGridTotals(totals);
            }
        }

        // TODO: Convert gameobject to entity
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            throw new System.NotImplementedException();
        }

        protected virtual IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return new BlockBehaviourBase[]
            {
                physicalBehaviour
            };
        }

        protected virtual void OnAdd()
        {
        }

        protected virtual void OnRemove()
        {
        }
    }
}