using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Ships;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.Gameplay
{
    public class Debris : MonoBehaviour, IGridInstance
    {
        [SerializeField] private Rigidbody2D rb;

        public BlockGrid BlockGrid { get; set; }
        public Rigidbody2D Rigidbody2D => rb;
        public Transform Transform => transform;
        public BlockContext BlockContext => BlockContext.Debris;
        public GridInstanceConfiguration Configuration { get; private set; }

        public void FromCluster(Cluster cluster) {
            BlockGrid = new BlockGrid(transform, this);
            Configuration = new GridInstanceConfiguration {
                Invulnerable = false
            };

            foreach (var block in cluster) {
                block.transform.SetParent(transform);
                block.Parent = this;
                BlockGrid.Add(block);
            }

            foreach (var thruster in BlockGrid.Metadata.ThrusterList) {
                thruster.PowerDown();
            }
        }
    }
}