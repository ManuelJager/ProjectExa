using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using UnityEngine;

namespace Exa.Utils
{
    public static class PhysicsExtensions
    {
        public static IEnumerable<RaycastBlockHit> RaycastAll(Vector2 start, Vector2 direction, float distance, ContextMask contextMask) {
            return from hit in Physics2D.RaycastAll(start, direction, distance, contextMask.LayerMask) 
                let block = hit.collider.transform.GetComponent<Block>() 
                where block != null && contextMask.HasValue(block.Parent.BlockContext) 
                select new RaycastBlockHit { hit = hit, block = block };
        }
    }

    public struct RaycastBlockHit
    {
        public RaycastHit2D hit;
        public Block block;
    }
}