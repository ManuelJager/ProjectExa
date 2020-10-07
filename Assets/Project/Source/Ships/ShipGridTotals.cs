using Exa.Grids;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipGridTotals : GridTotals
    {
        private readonly Ship _ship;

        public ShipGridTotals(Ship ship)
        {
            this._ship = ship;
        }

        public override float Mass
        {
            set
            {
                _ship.rb.mass = value;
                base.Mass = value;
            }
        }
    }
}