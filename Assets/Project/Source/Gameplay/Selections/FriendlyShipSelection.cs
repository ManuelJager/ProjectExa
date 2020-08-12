using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        public FriendlyShipSelection()
        {
            CanControl = true;
        }

        public void MoveTo(Vector2 position)
        {
            //var delta = position - (Vector2)friendlyShip.transform.position;
            //friendlyShip.transform.right = delta;
            //friendlyShip.transform.position = position;
        }

        public void LookAt(Vector2 position)
        {
            foreach (var ship in this)
            {
                ship.shipAI.lookAtTarget.Target = position;
            }
        }

        public override ShipSelection Clone()
        {
            var selection = new FriendlyShipSelection();
            foreach (var ship in this)
            {
                selection.Add(ship);
            }
            return selection;
        }
    }
}