using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Grids
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] protected ShipGrid shipGrid;

        public virtual void Import(Blueprint blueprint)
        {
            shipGrid.Import(blueprint);
        }
    }
}