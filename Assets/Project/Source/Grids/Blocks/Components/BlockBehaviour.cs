using Exa.Ships;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : IBlockComponentData
    {
        protected T data;
        protected Ship ship;

        public override Ship Ship
        {
            get => ship;
            set
            {
                if (ship == value) return;

                if (ship != null)
                {
                    OnRemove();
                }

                ship = value;

                if (ship != null)
                {
                    OnAdd();
                }
            }
        }

        public T Data
        {
            get => data;
            set => data = value;
        }

        public override IBlockComponentData BlockComponentData => data;

        protected virtual void OnAdd()
        {
            
        }

        protected virtual void OnRemove()
        {
            
        }
    }

    public abstract class BlockBehaviourBase : MonoBehaviour
    {
        [HideInInspector] public Block block;

        public abstract Ship Ship { get; set; }

        public abstract IBlockComponentData BlockComponentData { get; }
    }
}