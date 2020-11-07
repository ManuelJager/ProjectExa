using Exa.Grids.Blocks;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay
{
    public class Debris : MonoBehaviour
    {
        public BlockGrid BlockGrid { get; set; }

        public void FromCluster(Cluster cluster) {
            BlockGrid = new BlockGrid(transform, null, BlockContext.Debris);

            foreach (var block in cluster) {
                block.transform.SetParent(transform);
            }
        }
    }
}