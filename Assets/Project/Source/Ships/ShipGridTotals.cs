using Exa.Grids;

namespace Exa.Ships
{
    public class ShipGridTotals : GridTotals
    {
        private readonly Ship ship;

        public ShipGridTotals(Ship ship) {
            this.ship = ship;
        }
    }
}