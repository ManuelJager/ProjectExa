﻿using Exa.Ships;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Unity.Entities;
using UnityEngine;
using System.Collections.Generic;
using Exa.Utils;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IBlock, IConvertGameObjectToEntity, IGridMember, IPhysical
    {
        [HideInInspector] public AnchoredBlueprintBlock anchoredBlueprintBlock;

        [SerializeField] private PhysicalBehaviour physicalBehaviour;
        private BlockGrid blockGrid;

        public Vector2Int GridAnchor => anchoredBlueprintBlock.gridAnchor;

        public BlueprintBlock BlueprintBlock => anchoredBlueprintBlock.blueprintBlock;

        BlockBehaviour<PhysicalData> IBehaviourMarker<PhysicalData>.Component {
            get => physicalBehaviour;
        }

        public PhysicalBehaviour PhysicalBehaviour => physicalBehaviour;

        public BlockGrid BlockGrid {
            get => blockGrid;
            set {
                if (blockGrid == value) return;

                if (blockGrid != null && !Systems.IsQuitting) {
                    OnRemove();
                }

                blockGrid = value;

                if (blockGrid != null) {
                    OnAdd();
                }

                foreach (var behaviour in GetBehaviours()) {
                    behaviour.BlockGrid = value;
                }
            }
        }

        private void OnDisable() {
            if (Systems.IsQuitting) return;

            blockGrid.Remove(GridAnchor);
            BlockGrid = null;
        }

        public TComponent GetBlockComponent<TComponent, TValues>()
            where TComponent : BlockBehaviour<TValues>
            where TValues : struct, IBlockComponentValues {
            if (this is IBehaviourMarker<TValues> behaviourMarker && behaviourMarker.Component is TComponent behaviour) {
                return behaviour;
            }

            return null;
        }

        public bool TryGetBlockComponent<TComponent, TValues>(out TComponent value)
            where TComponent : BlockBehaviour<TValues>
            where TValues : struct, IBlockComponentValues {
            value = GetBlockComponent<TComponent, TValues>();
            return value != null;
        }

        public void AddGridTotals(GridTotals totals) {
            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.RemoveGridTotals(totals);
            }
        }

        // TODO: Convert gameobject to entity
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
            throw new System.NotImplementedException();
        }

        // TODO: cache the result of this operation
        public virtual IEnumerable<BlockBehaviourBase> GetBehaviours() {
            return new BlockBehaviourBase[] {
                physicalBehaviour
            };
        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }
    }
}