using Exa.Grids;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject friendlyShipPrefab;

        public FriendlyShip CreateFriendly(string name)
        {
            var shipGO = Instantiate(friendlyShipPrefab);
            return Configure<FriendlyShip>(shipGO, name);
        }

        private T Configure<T>(GameObject shipGO, string name)
            where T : Ship
        {
            var ship = shipGO.GetComponent<T>();

            var blueprint = Systems.BlueprintManager.GetBlueprint(name);
            ship.Import(blueprint.Data);

            return ship;
        }
    }
}