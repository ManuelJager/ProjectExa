using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : BlockBehaviourBase
        where T : IBlockComponentData
    {
        public T data;

        public override void SetData(object data)
        {
            this.data = (T)data;
        }
    }

    public abstract class BlockBehaviourBase : MonoBehaviour
    {
        [HideInInspector] public Block block;

        public abstract void SetData(object data);
    }
}