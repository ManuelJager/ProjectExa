using Exa.Ships;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : IBlockComponentData
    {
        public T data;
        protected Ship ship;

        public Ship Ship
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

        public override void SetData(object data)
        {
            this.data = (T)data;
        }

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

        public abstract void SetData(object data);
    }
}