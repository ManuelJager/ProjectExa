using System;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class BlockBehaviour<T> : BlockBehaviour
        where T : struct, IBlockComponentValues {
        [NonSerialized] protected T data;

        public T Data {
            get => data;
            set => data = value;
        }

        public override IBlockComponentValues BlockComponentData {
            get => data;
            set => data = (T) value;
        }

        public T GetDefaultData() {
            var context = block.Parent.BlockContext;
            var template = block.BlueprintBlock.Template;
            var store = S.Blocks.Values;
            var success = store.TryGetValues<T>(context, template, out var result);

            return success ? result : throw new InvalidOperationException("Could not find default values");
        }
    }

    public abstract class BlockBehaviour : MonoBehaviour {
        [HideInInspector] public Block block;
        private bool forceActive;
        private IGridInstance parent;

        public abstract IBlockComponentValues BlockComponentData { get; set; }

        public IGridInstance Parent {
            get => parent;
            set {
                if (parent == value) {
                    return;
                }

                if (parent != null) {
                    OnRemove();
                }

                parent = value;

                if (parent != null) {
                    OnAdd();
                }
            }
        }

        public GridInstance GridInstance {
            get => Parent as GridInstance;
        }

        private void Update() {
            if (forceActive || GridInstance && GridInstance.Active) {
                BlockUpdate();
            }
        }

        protected virtual void BlockUpdate() { }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }

        public void ForceActive() {
            forceActive = true;
        }
    }
}