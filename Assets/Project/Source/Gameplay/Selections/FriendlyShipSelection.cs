using System;
using System.Linq;
using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        public FriendlyShipSelection()
        {
            CanControl = true;
        }

        public void MoveLookAt(Vector2 point, Formation formation)
        {
            var formationEnumerator = formation.GetGlobalLayout(this, point).GetEnumerator();

            foreach (var ship in this.OrderByDescending((ship) => ship.Blueprint.Blocks.MaxSize))
            {
                formationEnumerator.MoveNext();
                var formationPosition = formationEnumerator.Current;

                ship.shipAI.moveToTarget.Target = formationPosition;
                ship.shipAI.lookAtTarget.Target = formationPosition;
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