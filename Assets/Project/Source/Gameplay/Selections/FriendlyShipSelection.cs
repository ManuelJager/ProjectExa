using System.Linq;
using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        public FriendlyShipSelection(Formation formation)
            : base(formation)
        {
            CanControl = true;
        }

        public void MoveLookAt(Vector2 point)
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
            var selection = new FriendlyShipSelection(formation);
            foreach (var ship in this)
            {
                selection.Add(ship);
            }
            return selection;
        }
    }
}