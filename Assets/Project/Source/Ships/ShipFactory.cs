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

        public PlayerStationGridInstance CreateStation(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration configuration) {
            return Configure<PlayerStationGridInstance>(
                prefab: friendlyStationPrefab,
                container: GameSystems.SpawnLayer.ships, 
                worldPos: worldPos,
                blueprint: blueprint, 
                blockContext: BlockContext.UserGroup,
                configuration: configuration);
        }

        public EnemyGridInstance CreateEnemy(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration configuration) {
            return Configure<EnemyGridInstance>(
                prefab: enemyShipPrefab,
                container: GameSystems.SpawnLayer.ships,
                worldPos: worldPos,
                blueprint: blueprint,
                blockContext: BlockContext.EnemyGroup,
                configuration: configuration);
        }

        private T Configure<T>(GameObject prefab, Transform container, Vector2 worldPos, Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration)
            where T : GridInstance {
            var gridGo = prefab.InstantiateAndGet<T>(container);
            gridGo.SetPosition(worldPos);
            var overlay = CreateOverlay(gridGo);
            gridGo.Import(blueprint, blockContext, configuration);

            var instanceString = gridGo.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            gridGo.name = instanceString;

            return gridGo;
        }

        private ShipOverlay CreateOverlay(GridInstance gridInstance) {
            var overlayGo = Instantiate(shipOverlayPrefab, overlayContainer);
            gridInstance.ControllerDestroyedEvent.AddListener(() => {
                Destroy(overlayGo);
            });

            var overlay = overlayGo.GetComponent<ShipOverlay>();
            overlay.gridInstance = gridInstance;
            gridInstance.Overlay = overlay;
            return overlay;
        }
    }
}