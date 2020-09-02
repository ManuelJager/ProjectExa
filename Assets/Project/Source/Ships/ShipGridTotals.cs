using Exa.Grids;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipGridTotals : GridTotals
    {
        private readonly Ship ship;

        public ShipGridTotals(Ship ship)
        {
            this.ship = ship;
        }

        public override long Mass
        {
            get => base.Mass;
            set
            {
                ship.rb.mass = value / 1000f;
                base.Mass = value;
            }
        }
    }
}