using System.Runtime.CompilerServices;
using Exa.Ships;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : struct, IBlockComponentValues
    {
        protected T data;

        public T Data {
            get => data;
            set => data = value;
        }

        public override IBlockComponentValues BlockComponentData => data;
    }

    public abstract class BlockBehaviourBase : MonoBehaviour
    {
        [HideInInspector] public Block block;
        protected BlockGrid blockGrid;

        public abstract IBlockComponentValues BlockComponentData { get; }

        public BlockGrid BlockGrid
        {
            get => blockGrid;
            set
            {
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

        private void Update() {
            if (blockGrid?.Ship != null && blockGrid.Ship.Active)
                BlockUpdate();
        }

        protected virtual void BlockUpdate() {

        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }
    }
}