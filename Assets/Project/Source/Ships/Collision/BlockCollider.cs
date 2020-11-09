using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships
{
    public class BlockCollider : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private Block block;

        public BoxCollider2D Collider {
            get => boxCollider2D;
        }

        public Block Block {
            get => block;
            set => block = value;
        }
    }
}