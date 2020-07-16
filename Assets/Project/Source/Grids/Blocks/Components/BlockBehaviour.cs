using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class BlockBehaviour<T> : MonoBehaviour
        where T : IBlockComponentData
    {
        [HideInInspector] public Block block;
        public T data;
    }
}