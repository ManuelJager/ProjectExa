using System;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Ships
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject friendlyStationPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;
        [SerializeField] private Transform overlayContainer;

        public PlayerStation CreateStation(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration? configuration = null) {
            return Configure<PlayerStation>(
                prefab: friendlyStationPrefab,
                container: GameSystems.SpawnLayer.ships, 
                worldPos: worldPos,
                blueprint: blueprint, 
                blockContext: BlockContext.UserGroup,
                configuration: configuration);
        }

        public EnemyGrid CreateEnemy(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration? configuration = null) {
            return Configure<EnemyGrid>(
                prefab: enemyShipPrefab,
                container: GameSystems.SpawnLayer.ships,
                worldPos: worldPos,
                blueprint: blueprint,
                blockContext: BlockContext.EnemyGroup,
                configuration: configuration);
        }

        private T Configure<T>(GameObject prefab, Transform container, Vector2 worldPos, Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration? configuration)
            where T : GridInstance {
            var grid = prefab.Create<T>(container);
            grid.Import(blueprint, blockContext, configuration ?? new GridInstanceConfiguration {
                Invulnerable = false
            });
            grid.SetPosition(worldPos);
            grid.name = grid.GetInstanceString();
            
            Physics2D.SyncTransforms();

            return grid;
        }

        public GridOverlay CreateOverlay(GridInstance gridInstance) {
            var overlayGo = Instantiate(shipOverlayPrefab, overlayContainer);
            gridInstance.ControllerDestroyedEvent.AddListener(() => {
                Destroy(overlayGo);
            });

            var overlay = overlayGo.GetComponent<GridOverlay>();
            overlay.SetGrid(gridInstance);
            overlay.gameObject.name = $"Overlay: {gridInstance.GetInstanceString()}";
            return overlay;
        }
    }
}