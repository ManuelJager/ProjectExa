using Exa.Grids;
using UnityEngine;

namespace Exa.Ships
{
    public class ShipGridTotals : GridTotals
    {
        private Rigidbody rb;

        public ShipGridTotals(Rigidbody rb)
        {
            this.rb = rb;
        }

        public new long Mass
        {
            get => base.Mass;
            set
            {
                rb.mass = value / 1000f;
                base.Mass = value;
            }
        }
    }
}