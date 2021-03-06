﻿using Exa.Math;
using Exa.Ships.Targeting;
using System.Linq;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay
{
    public class FriendlyShipSelection : ShipSelection
    {
        public FriendlyShipSelection(Formation formation)
            : base(formation) {
            CanControl = true;
        }

        public void MoveLookAt(Vector2 point) {
            var formationPositions = formation.GetGlobalLayout(this, point);
            var formationShips = this.OrderByDescending(ship => ship.Blueprint.Blocks.MaxSize);

            foreach (var (ship, formationPosition) in formationShips.AsTupleEnumerable(formationPositions)) {
                var currentPosition = ship.transform.position.ToVector2();

                ship.Ai.moveToTarget.Target = new StaticPositionTarget(formationPosition);
                ship.Ai.lookAtTarget.Target = new StaticAngleTarget(currentPosition, formationPosition);
            }
        }

        public override ShipSelection Clone() {
            var selection = new FriendlyShipSelection(formation);
            foreach (var ship in this) {
                selection.Add(ship);
            }

            return selection;
        }
    }
}