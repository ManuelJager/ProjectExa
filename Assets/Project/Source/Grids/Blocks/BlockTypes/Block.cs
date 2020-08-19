using Exa.Ships;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;
using Exa.Debugging;

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IBlock, IConvertGameObjectToEntity, IGridMember, IPhysical
    {
        [HideInInspector] public AnchoredBlueprintBlock anchoredBlueprintBlock;

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
                if (ship == value) return;

                if (ship != null && !Systems.IsQuitting)
                {
                    OnRemove();
                }

                ship = value;

                if (ship != null)
                {
                    OnAdd();
                }

                foreach (var behaviour in GetBehaviours())
                {
                    behaviour.Ship = value;
                }
            }
        }

        private void OnDisable()
        {
            if (Systems.IsQuitting) return;

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

        // TODO: cache the result of this operation
        public virtual IEnumerable<BlockBehaviourBase> GetBehaviours()
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