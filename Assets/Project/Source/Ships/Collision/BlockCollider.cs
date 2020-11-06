using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockCollider : MonoBehaviour
    {
        [SerializeField] private Block block;

        public Block Block {
            get => block;
            set => block = value;
        }
    }
}