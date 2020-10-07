using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _friendlyShipPrefab;
        [SerializeField] private GameObject _enemyShipPrefab;
        [SerializeField] private GameObject _shipOverlayPrefab;
        [SerializeField] private Transform _shipContainer;
        [SerializeField] private Transform _overlayContainer;

        public FriendlyShip CreateFriendly(string blueprintName, Vector2 worldPos)
        {
            var shipGo = Instantiate(_friendlyShipPrefab, _shipContainer);
            shipGo.transform.position = worldPos;

            return Configure<FriendlyShip>(shipGo, blueprintName, ShipContext.UserGroup);
        }

        public EnemyShip CreateEnemy(string blueprintName, Vector2 worldPos)
        {
            var shipGo = Instantiate(_enemyShipPrefab, _shipContainer);
            shipGo.transform.position = worldPos;

            return Configure<EnemyShip>(shipGo, blueprintName, ShipContext.EnemyGroup);
        }

        private T Configure<T>(GameObject shipGo, string name, ShipContext blockContext)
            where T : Ship
        {
            var ship = shipGo.GetComponent<T>();
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint.Data, blockContext);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGo.name = $"{nameof(T)} - {instanceString}";

            return ship;
        }

        private ShipOverlay CreateOverlay(Ship ship)
        {
            var overlayGo = Instantiate(_shipOverlayPrefab, _overlayContainer);
            var overlay = overlayGo.GetComponent<ShipOverlay>();
            overlay.ship = ship;
            ship.overlay = overlay;
            return overlay;
        }
    }
}