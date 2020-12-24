using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using Exa.Data;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    // TODO: Clamp the requested thrust vector to what the Ship can output
    public class ThrustVectors : IThrustVectors
    {
        private readonly Dictionary<int, ThrusterGroup> thrusterDict;

        public ThrustVectors(Scalar thrustModifier) {
            thrusterDict = new Dictionary<int, ThrusterGroup> {
                {0, new ThrusterGroup(thrustModifier)},
                {1, new ThrusterGroup(thrustModifier)},
                {2, new ThrusterGroup(thrustModifier)},
                {3, new ThrusterGroup(thrustModifier)}
            };
        }

        public void Register(IThruster thruster) {
            SelectGroup(thruster)?.Add(thruster);
        }

        public void Unregister(IThruster thruster) {
            SelectGroup(thruster)?.Remove(thruster);
        }

        /// <summary>
        /// Sets the graphics using a local space scalar vector
        /// </summary>
        /// <param name="directionScalar"></param>
        public void SetGraphics(Vector2 directionScalar) {
            SelectHorizontalGroup(directionScalar.x, false).SetGraphics(Mathf.Abs(directionScalar.x));
            SelectHorizontalGroup(directionScalar.x, true).SetGraphics(0);
            SelectVerticalGroup(directionScalar.y, false).SetGraphics(Mathf.Abs(directionScalar.y));
            SelectVerticalGroup(directionScalar.y, true).SetGraphics(0);
        }

        private ThrusterGroup SelectGroup(IThruster thruster) {
            var rotation = GetDirection(thruster);
            try {
                return thrusterDict[rotation];
            }
            catch (KeyNotFoundException) {
                Debug.LogWarning(
                    $"thruster {thruster} with rotation {rotation} cannot find a corresponding thruster group");
                return null;
            }
        }

        private ThrusterGroup SelectHorizontalGroup(float x, bool revert) =>
            x > 0 ^ revert
                ? thrusterDict[0]
                : thrusterDict[2];

        private ThrusterGroup SelectVerticalGroup(float y, bool revert) =>
            y > 0 ^ revert
                ? thrusterDict[1]
                : thrusterDict[3];

        private int GetDirection(IThruster thruster) {
            return thruster.Component.block.anchoredBlueprintBlock.blueprintBlock.Direction;
        }
    }
}