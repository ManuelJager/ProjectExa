using Exa.Math;
using Exa.Ships.Targetting;
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
                var currentPosition = ship.transform.position.ToVector2();
                var formationPosition = formationEnumerator.Current;

                ship.shipAI.moveToTarget.Target = new StaticPositionTarget(formationPosition);
                ship.shipAI.lookAtTarget.Target = new StaticAngleTarget(currentPosition, formationPosition);
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