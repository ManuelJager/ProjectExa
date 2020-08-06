using Exa.Grids;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        private FriendlyShip friendlyShip;

        public FriendlyShipSelection(FriendlyShip friendlyShip)
        {
            this.friendlyShip = friendlyShip;

            CanControl = true;
        }

        public override void MoveTo(Vector2 position)
        {
            var delta = position - (Vector2)friendlyShip.transform.position;
            friendlyShip.transform.rotation.SetLookRotation(delta);
            friendlyShip.transform.position = position;
        }
    }
}
