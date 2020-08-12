using UnityEngine;

namespace Exa.Ships
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject friendlyShipPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;
        [SerializeField] private Transform shipContainer;
        [SerializeField] private Transform overlayContainer;

        public FriendlyShip CreateFriendly(string name, Vector2 worldPos = new Vector2())
        {
            var shipGO = Instantiate(friendlyShipPrefab, shipContainer);
            shipGO.transform.position = worldPos;
            return Configure<FriendlyShip>(shipGO, name);
        }

        private T Configure<T>(GameObject shipGO, string name)
            where T : Ship
        {
            var ship = shipGO.GetComponent<T>();
            var blueprint = Systems.Blueprints.GetBlueprint(name);
            var overlay = CreateOverlay(ship);
            ship.Import(blueprint.Data);

            var instanceString = ship.GetInstanceString();
            overlay.gameObject.name = $"Overlay: {instanceString}";
            shipGO.name = $"{typeof(T).Name} - {instanceString}";

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