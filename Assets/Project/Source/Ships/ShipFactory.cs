using System;
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

        public FriendlyShip CreateFriendly(Blueprint blueprint, Vector2 worldPos) {
            var shipGo = Instantiate(friendlyShipPrefab, GameSystems.SpawnLayer.ships);
            return Configure<FriendlyShip>(shipGo, worldPos, blueprint, ShipContext.UserGroup);
        }

        public EnemyShip CreateEnemy(Blueprint blueprint, Vector2 worldPos) {
            var shipGo = Instantiate(enemyShipPrefab, GameSystems.SpawnLayer.ships);
            return Configure<EnemyShip>(shipGo, worldPos, blueprint, ShipContext.EnemyGroup);
        }

        private T Configure<T>(GameObject shipGo, Vector2 worldPos, Blueprint blueprint, ShipContext blockContext)
            where T : Ship {
            shipGo.transform.position = worldPos;
            var ship = shipGo.GetComponent<T>();
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint, blockContext);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGo.name = $"{nameof(T)} - {instanceString}";

            return ship;
        }

        private ShipOverlay CreateOverlay(Ship ship) {
            var overlayGo = Instantiate(shipOverlayPrefab, overlayContainer);
            var overlay = overlayGo.GetComponent<ShipOverlay>();
            overlay.ship = ship;
            ship.overlay = overlay;
            return overlay;
        }
    }
}