using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Utils;

namespace Exa.Gameplay
{
    public class BlockGridManager : MonoBehaviour
    {
        [SerializeField] private GameObject debrisGridPrefab;

        public void AttemptRebuild(IGridInstance gridInstance) {
            var blockGrid = gridInstance.BlockGrid;
            if (!blockGrid.Any()) 
                return;

            var clusters = GetClusters(blockGrid);
            if (clusters.Count() > 1)
                Rebuild(gridInstance, clusters);
        }

        private void Rebuild(IGridInstance gridInstance, IEnumerable<Cluster> clusters) {
            var blockGrid = gridInstance.BlockGrid;
            blockGrid.Rebuilding = true;
            var clustersScheduledForRebuild = clusters.Where(cluster => !cluster.containsController);

            foreach (var cluster in clustersScheduledForRebuild) {
                var debris = debrisGridPrefab.Create<Debris>(transform);
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
            var visitedGridPos = new HashSet<Vector2Int>();

            void FloodFill(int x, int y) {
                var gridPos = new Vector2Int(x, y);
                
                if (!visitedGridPos.Add(gridPos)) return;
                if (!blockGrid.TryGetMember(gridPos, out var member)) return;
                if (vacantBlocks.Contains(member)) {
                    vacantBlocks.Remove(member);
                    cluster.Add(member);

                    if (member.GetIsController()) {
                        if (cluster.containsController)
                            Debug.LogWarning($"Cluster {cluster} contains multiple controllers, this shouldn't happen");

                        cluster.containsController = true;
                    }
                }

                FloodFill(x - 1, y);
                FloodFill(x + 1, y);
                FloodFill(x, y - 1);
                FloodFill(x, y + 1);
            }

            FloodFill(startPoint.x, startPoint.y);

            return cluster;
        }
    }
}
