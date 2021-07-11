using System;
using System.Collections.Generic;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes {
    /// <summary>
    ///     Base class for blocks
    /// </summary>
    public class Block : MonoBehaviour, IGridMember {
        [HideInInspector] public BlockPoolMember blockPoolMember;

        [SerializeField] private PhysicalBehaviour physicalBehaviour;
        [SerializeField] [HideInInspector] private new BoxCollider2D collider;
        [NonSerialized] public ABpBlock aBpBlock;
        private IGridInstance parent;

        private List<BlockBehaviour> behaviours;

        public PhysicalBehaviour PhysicalBehaviour {
            get => physicalBehaviour;
        }

        public BoxCollider2D Collider {
            get => collider;
            set => collider = value;
        }

        public IGridInstance Parent {
            get => parent;
            set {
                if (parent == value) {
                    return;
                }

                if (parent != null && !S.IsQuitting) {
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

        public GridInstance GridInstance {
            get => Parent as GridInstance;
        }

        public Vector2Int GridAnchor {
            get => aBpBlock.gridAnchor;
        }

        public BlueprintBlock BlueprintBlock {
            get => aBpBlock.blueprintBlock;
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

        public bool Equals(IGridMember other) {
            return IGridMemberComparer.Default.Equals(this, other);
        }

        /// <summary>
        ///     Returns the block to the pool
        /// </summary>
        public void DestroyBlock() {
            if (GS.IsQuitting) {
                parent = null;

                return;
            }

            Parent.BlockGrid.Remove(GridAnchor);
            Parent = null;
            blockPoolMember.ReturnBlock();
        }

        public T GetBehaviour<T>() {
            return GetBehaviours().FindFirst<T>();
        }

        public BlockBehaviour<T> GetBehaviourOfData<T>()
            where T : struct, IBlockComponentValues {
            foreach (var behaviour in GetBehaviours()) {
                if (behaviour.GetDataTypeIsOf<T>()) {
                    return (BlockBehaviour<T>)behaviour;
                }
            }
            
            return null;
        }

        public bool TryGetBehaviour<T>(out T value) {
            value = GetBehaviour<T>();

            return value.Equals(default);
        }

        public IEnumerable<BlockBehaviour> GetBehaviours() {
            if (behaviours == null) {
                gameObject.GetComponents(behaviours = new List<BlockBehaviour>());
            }

            return behaviours;
        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        public override string ToString() {
            return $"Block: {BlueprintBlock.Template.id}";
        }
    }
}