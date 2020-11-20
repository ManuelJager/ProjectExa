using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Utils;

namespace Exa.Gameplay
{
    public class BlockGridManager : MonoBehaviour
    {
        [SerializeField] private GameObject debrisGridPrefab;

        public void MarkDirty(IGridInstance gridInstance) {
            var blockGrid = gridInstance.BlockGrid;
            if (!blockGrid.Any()) 
                return;

            var clusters = GetClusters(gridInstance.BlockGrid);
            if (clusters.Count() > 1)
                Rebuild(gridInstance, clusters);
        }

        private void Rebuild(IGridInstance gridInstance, IEnumerable<Cluster> clusters) {
            var blockGrid = gridInstance.BlockGrid;
            blockGrid.Rebuilding = true;
            var clustersScheduledForRebuild = clusters.Where(cluster => !cluster.containsController);
            foreach (var cluster in clustersScheduledForRebuild) {
                var debris = Instantiate(debrisGridPrefab, transform).GetComponent<Debris>();
                debris.FromCluster(cluster);
                debris.Rigidbody2D.velocity = gridInstance.Rigidbody2D.velocity;
            }
            blockGrid.Rebuilding = false;
            blockGrid.DestroyIfEmpty();
        }

        private IEnumerable<Cluster> GetClusters(BlockGrid blockGrid) {
            var vacantBlocks = new List<Block>(blockGrid);

            while (vacantBlocks.Count > 0) {
                var firstBlock = vacantBlocks.First();
                yield return GetCluster(firstBlock.GridAnchor, vacantBlocks, blockGrid);
            }
        }

        private Cluster GetCluster(Vector2Int startPoint, ICollection<Block> vacantBlocks, BlockGrid blockGrid) {
            var cluster = new Cluster();

            void FloodFill(Vector2Int gridPos) {
                if (blockGrid.TryGetMember(gridPos, out var member) && vacantBlocks.Contains(member)) {
                    vacantBlocks.Remove(member);
                    cluster.Add(member);

                    if (member.GetIsController()) {
                        if (cluster.containsController)
                            Debug.LogWarning($"Cluster {cluster} contains multiple controllers, this shouldn't happen");

                        cluster.containsController = true;
                    }

                    FloodFill(new Vector2Int(gridPos.x - 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x + 1, gridPos.y));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y - 1));
                    FloodFill(new Vector2Int(gridPos.x, gridPos.y + 1));
                }
            }

            FloodFill(startPoint);

            return cluster;
        }
    }
}
