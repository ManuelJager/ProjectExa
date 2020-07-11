using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class Ship : MonoBehaviour
    {
        private Blueprint blueprint;
        [SerializeField] private ShipGrid shipGrid;

        public void Import(Blueprint blueprint)
        {
            this.blueprint = blueprint;
            shipGrid.Import(blueprint);
        }
    }
}