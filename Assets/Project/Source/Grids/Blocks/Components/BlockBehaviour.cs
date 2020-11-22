using System.Runtime.CompilerServices;
using Exa.Ships;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : struct, IBlockComponentValues
    {
        [SerializeField] protected T data;

        public T Data {
            get => data;
            set => data = value;
        }

        public override IBlockComponentValues BlockComponentData => data;
    }

    public abstract class BlockBehaviourBase : MonoBehaviour
    {
        [HideInInspector] public Block block;
        private IGridInstance parent;

        public abstract IBlockComponentValues BlockComponentData { get; }

        public IGridInstance Parent
        {
            get => parent;
            set
            {
                if (parent == value) return;

                if (parent != null) {
                    OnRemove();
                }

                parent = value;

                if (parent != null) {
                    OnAdd();
                }
            }
        }

        public Ship Ship => Parent as Ship;

        private void Update() {
            if (Ship && Ship.Active)
                BlockUpdate();
        }

        protected virtual void BlockUpdate() {

        }

        protected virtual void OnAdd() { }

        protected virtual void OnRemove() { }
    }
}