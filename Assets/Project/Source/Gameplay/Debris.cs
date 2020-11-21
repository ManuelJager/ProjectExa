using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
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

        public void FromCluster(Cluster cluster) {
            BlockGrid = new BlockGrid(transform, OnGridEmpty, this);

            foreach (var block in cluster) {
                block.transform.SetParent(transform);
                block.Parent = this;
                BlockGrid.Add(block);
            }

            foreach (var thruster in BlockGrid.Metadata.QueryByType<IThruster>()) {
                thruster.PowerDown();
            }
        }

        private void OnGridEmpty() {
            if (gameObject)
                Destroy(gameObject);
        }
    }
}