using Exa.Grids;
using UnityEngine;

namespace Exa.Gameplay
{
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab;

        public Ship Create(string name)
        {
            var shipGO = Instantiate(shipPrefab);
            var ship = shipGO.GetComponent<Ship>();

            var blueprint = Systems.BlueprintManager.GetBlueprint(name);
            ship.Import(blueprint.Data);

            return ship;
        }
    }
}