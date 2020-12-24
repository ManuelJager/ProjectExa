using Exa.Math;
using Exa.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Gameplay
{
    public abstract class Formation
    {
        protected abstract IEnumerable<Vector2> GetLocalLayout(IEnumerable<GridInstance> ships);

        public IEnumerable<Vector2> GetGlobalLayout(ShipSelection ships, Vector2 point) {
            if (!ships.Any())
                throw new ArgumentException("Cannot layout an empty collection", nameof(ships));

            // Direction is right aligned, so we subtract 90 degrees to correct
            var direction = point - ships.AveragePosition;
            var angle = direction.GetAngle();

            foreach (var localShipPos in GetLocalLayout(ships)) {
                yield return localShipPos.Rotate(angle) + point;
            }
        }
    }
}