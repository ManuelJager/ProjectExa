using Exa.Math;
using Exa.Ships;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Gameplay
{
    public class VicFormation : Formation
    {
        private readonly float echelonAngle;

        public VicFormation(float echelonAngle = -130f) {
            this.echelonAngle = echelonAngle;
        }

        protected override IEnumerable<Vector2> GetLocalLayout(IEnumerable<GridInstance> ships) {
            var enumerator = ships.GetEnumerator();

            // Skip first element, as it always has a Vector2.zero position value
            enumerator.MoveNext();
            var firstShip = enumerator.Current;
            yield return Vector2.zero;

            var echelonSpread = CalculateEchelonSpread(firstShip);
            var rightEchelonSize = echelonSpread;
            var leftEchelonSize = echelonSpread;

            // Handle echelon layout
            var rightEchelonPivot = Vector2.zero;
            var leftEchelonPivot = Vector2.zero;

            while (enumerator.MoveNext()) {
                // Get right echelon position
                yield return GetLocalPosition(enumerator.Current, ref rightEchelonSize, ref rightEchelonPivot,
                    echelonAngle);

                if (!enumerator.MoveNext()) break;

                // Get left echelon positon
                yield return GetLocalPosition(enumerator.Current, ref leftEchelonSize, ref leftEchelonPivot,
                    -echelonAngle);
            }
        }

        private Vector2 GetLocalPosition(GridInstance gridInstance, ref float echelonMagnitude, ref Vector2 positionPivot,
            float angle) {
            var positionOffset = MathUtils.FromAngledMagnitude(echelonMagnitude, angle);
            positionPivot += positionOffset;
            echelonMagnitude = CalculateEchelonSpread(gridInstance);
            return positionPivot;
        }

        private float CalculateEchelonSpread(GridInstance gridInstance) {
            return gridInstance.Blueprint.Blocks.MaxSize * 2f;
        }
    }
}