using Exa.Grids;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        private FriendlyShip friendlyShip;

        public FriendlyShipSelection(FriendlyShip friendlyShip)
        {
            this.friendlyShip = friendlyShip;
            Add(friendlyShip);
            CanControl = true;
        }

        public override void MoveTo(Vector2 position)
        {
            var delta = position - (Vector2)friendlyShip.transform.position;
            friendlyShip.transform.right = delta;
            friendlyShip.transform.position = position;
        }
    }
}