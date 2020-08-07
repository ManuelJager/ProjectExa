using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject friendlyShipPrefab;
        [SerializeField] private GameObject shipOverlayPrefab;
        [SerializeField] private Transform shipContainer;
        [SerializeField] private Transform overlayContainer;

        public FriendlyShip CreateFriendly(string name)
        {
            var shipGO = Instantiate(friendlyShipPrefab, shipContainer);
            return Configure<FriendlyShip>(shipGO, name);
        }

        private T Configure<T>(GameObject shipGO, string name)
            where T : Ship
        {
            var ship = shipGO.GetComponent<T>();
            var blueprint = Systems.BlueprintManager.GetBlueprint(name);
            CreateOverlay(ship);
            ship.Import(blueprint.Data);
            return ship;
        }

        private void CreateOverlay(Ship ship)
        {
            var overlayGO = Instantiate(shipOverlayPrefab, overlayContainer);
            var overlay = overlayGO.GetComponent<ShipOverlay>();
            overlay.ship = ship;
            ship.overlay = overlay;
        }
    }
}