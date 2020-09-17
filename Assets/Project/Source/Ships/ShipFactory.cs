using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject friendlyShipPrefab;
        [SerializeField] private GameObject enemyShipPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;
        [SerializeField] private Transform shipContainer;
        [SerializeField] private Transform overlayContainer;

        public FriendlyShip CreateFriendly(string blueprintName, Vector2 worldPos)
        {
            var shipGO = Instantiate(friendlyShipPrefab, shipContainer);
            shipGO.transform.position = worldPos;

            return Configure<FriendlyShip>(shipGO, blueprintName, ShipContext.UserGroup);
        }

        public EnemyShip CreateEnemy(string blueprintName, Vector2 worldPos)
        {
            var shipGO = Instantiate(enemyShipPrefab, shipContainer);
            shipGO.transform.position = worldPos;

            return Configure<EnemyShip>(shipGO, blueprintName, ShipContext.EnemyGroup);
        }

        private T Configure<T>(GameObject shipGO, string name, ShipContext blockContext)
            where T : Ship
        {
            var ship = shipGO.GetComponent<T>();
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint.Data, blockContext);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGO.name = $"{nameof(T)} - {instanceString}";

            return ship;
        }

        private ShipOverlay CreateOverlay(Ship ship)
        {
            var overlayGO = Instantiate(shipOverlayPrefab, overlayContainer);
            var overlay = overlayGO.GetComponent<ShipOverlay>();
            overlay.ship = ship;
            ship.overlay = overlay;
            return overlay;
        }
    }
}