﻿using System;
using Exa.Ships;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using UnityEngine;
using System.Collections.Generic;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    /// <summary>
    /// Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IGridMember, IPhysical
    {
        [NonSerialized] public ABpBlock aBpBlock;
        [HideInInspector] public BlockPoolMember blockPoolMember;
        
        [SerializeField] private PhysicalBehaviour physicalBehaviour;
        [SerializeField, HideInInspector] private new BoxCollider2D collider;
        private IGridInstance parent;

        public Vector2Int GridAnchor => aBpBlock.gridAnchor;

        public BlueprintBlock BlueprintBlock => aBpBlock.blueprintBlock;

        BlockBehaviour<PhysicalData> IBehaviourMarker<PhysicalData>.Component => physicalBehaviour;

        public PhysicalBehaviour PhysicalBehaviour => physicalBehaviour;

        public BoxCollider2D Collider {
            get => collider;
            set => collider = value;
        }

        public IGridInstance Parent {
            get => parent;
            set {
                if (parent == value) return;

                if (parent != null && !Systems.IsQuitting) {
                    OnRemove();
                }

                parent = value;

                if (parent != null) {
                    OnAdd();
                }

                foreach (var behaviour in GetBehaviours()) {
                    behaviour.Parent = value;
                }
            }
        }

        public GridInstance GridInstance => Parent as GridInstance;

        /// <summary>
        /// Returns the block to the pool
        /// </summary>
        public void DestroyBlock() {
            if (GameSystems.IsQuitting) {
                parent = null;
                return;
            }

            Parent.BlockGrid.Remove(GridAnchor);
            Parent = null;
            blockPoolMember.ReturnBlock();
        }

        public TComponent GetBlockComponent<TComponent, TValues>()
            where TComponent : BlockBehaviour<TValues>
            where TValues : struct, IBlockComponentValues {
            return this is IBehaviourMarker<TValues> { Component: TComponent behaviour } ? behaviour : null;
        }

        public bool TryGetBlockComponent<TComponent, TValues>(out TComponent value)
            where TComponent : BlockBehaviour<TValues>
            where TValues : struct, IBlockComponentValues {
            value = GetBlockComponent<TComponent, TValues>();
            return value != null;
        }

        public void AddGridTotals(GridTotals totals) {
            totals.Metadata += BlueprintBlock.Template.metadata;
            
            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.AddGridTotals(totals);
            }
        }

        public void RemoveGridTotals(GridTotals totals) {
            totals.Metadata += BlueprintBlock.Template.metadata;
            
            foreach (var behaviour in GetBehaviours()) {
                behaviour.BlockComponentData.RemoveGridTotals(totals);
            }
        }

        // TODO: cache the result of this operation
        public virtual IEnumerable<BlockBehaviour> GetBehaviours() {
            return new BlockBehaviour[] {
                physicalBehaviour
            };
        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        public bool Equals(IGridMember other) {
            return IGridMemberComparer.Default.Equals(this, other);
        }

        public override string ToString() {
            return $"Block: {BlueprintBlock.Template.id}";
        }
    }
}