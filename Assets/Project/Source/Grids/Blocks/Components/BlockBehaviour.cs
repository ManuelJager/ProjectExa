using Exa.Ships;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : struct, IBlockComponentValues
    {
        protected T data;
        protected BlockGrid blockGrid;

        public override BlockGrid BlockGrid {
            get => blockGrid;
            set {
                if (blockGrid == value) return;

                if (blockGrid != null) {
                    OnRemove();
                }

                blockGrid = value;

                if (blockGrid != null) {
                    OnAdd();
                }
            }
        }

        public T Data {
            get => data;
            set => data = value;
        }

        public override IBlockComponentValues BlockComponentData => data;

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }
    }

    public abstract class BlockBehaviourBase : MonoBehaviour
    {
        [HideInInspector] public Block block;

        public abstract BlockGrid BlockGrid { get; set; }

        public abstract IBlockComponentValues BlockComponentData { get; }
    }
}