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
            var station = this.InstantiateAndGet<PlayerStationGridInstance>(friendlyStationPrefab, GameSystems.SpawnLayer.ships);
            station.Import(blueprint, BlockContext.UserGroup, configuration);
            station.SetPosition(worldPos);
            return station;
        }

        public EnemyGridInstance CreateEnemy(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration configuration) {
            var shipGo = Instantiate(enemyShipPrefab, GameSystems.SpawnLayer.ships);
            return Configure<EnemyGridInstance>(shipGo, worldPos, blueprint, BlockContext.EnemyGroup, configuration);
        }

        private T Configure<T>(GameObject shipGo, Vector2 worldPos, Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration)
            where T : GridInstance {
            shipGo.transform.position = worldPos;
            var ship = shipGo.GetComponent<T>();
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint, blockContext, configuration);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGo.name = instanceString;

            return ship;
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