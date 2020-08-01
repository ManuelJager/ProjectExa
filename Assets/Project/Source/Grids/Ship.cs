using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] protected ShipGrid shipGrid;
        protected Blueprint blueprint;

        public virtual void Import(Blueprint blueprint)
        {
            this.blueprint = blueprint;
            shipGrid.Import(blueprint);
        }
    }
}