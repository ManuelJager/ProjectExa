using System;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blueprints;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Ships
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject friendlyShipPrefab;
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;
        [SerializeField] private Transform overlayContainer;

        public FriendlyShip CreateFriendly(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration configuration) {
            var shipGo = Instantiate(friendlyShipPrefab, GameSystems.SpawnLayer.ships);
            return Configure<FriendlyShip>(shipGo, worldPos, blueprint, BlockContext.UserGroup, configuration);
        }

        public EnemyShip CreateEnemy(Blueprint blueprint, Vector2 worldPos, GridInstanceConfiguration configuration) {
            var shipGo = Instantiate(enemyShipPrefab, GameSystems.SpawnLayer.ships);
            return Configure<EnemyShip>(shipGo, worldPos, blueprint, BlockContext.EnemyGroup, configuration);
        }

        private T Configure<T>(GameObject shipGo, Vector2 worldPos, Blueprint blueprint, BlockContext blockContext, GridInstanceConfiguration configuration)
            where T : Ship {
            shipGo.transform.position = worldPos;
            var ship = shipGo.GetComponent<T>();
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint, blockContext, configuration);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGo.name = instanceString;

            return ship;
        }

        private ShipOverlay CreateOverlay(Ship ship) {
            var overlayGo = Instantiate(shipOverlayPrefab, overlayContainer);
            ship.ControllerDestroyedEvent.AddListener(() => {
                Destroy(overlayGo);
            });

            var overlay = overlayGo.GetComponent<ShipOverlay>();
            overlay.ship = ship;
            ship.Overlay = overlay;
            return overlay;
        }
    }
}