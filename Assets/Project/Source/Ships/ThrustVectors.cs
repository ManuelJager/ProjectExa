using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    // TODO: Rework this to hell and back
    // REVIEW: thrust vectors should have a default minimum value of thrust in each direction that's decided by the
    // ship controller
    public class ThrustVectors
    {
        private Dictionary<int, ThrusterGroup> thrusterDict;

        public ThrustVectors()
        {
            thrusterDict = new Dictionary<int, ThrusterGroup>
            {
                { 0, new ThrusterGroup() },
                { 1, new ThrusterGroup() },
                { 2, new ThrusterGroup() },
                { 3, new ThrusterGroup() }
            };
        }

        public void Register(Thruster thruster)
        {
            SelectGroup(thruster)?.Add(thruster);
        }

        public void Unregister(Thruster thruster)
        {
            SelectGroup(thruster)?.Remove(thruster);
        }

        public void Fire(Vector2 force)
        {
            SelectHorizontalGroup(force.x, false).Fire(Mathf.Abs(force.x));
            SelectHorizontalGroup(force.x, true).Fire(0);
            SelectVerticalGroup(force.y, false).Fire(Mathf.Abs(force.y));
            SelectVerticalGroup(force.y, true).Fire(0);
        }

        private ThrusterGroup SelectGroup(Thruster thruster)
        {
            var rotation = GetRotation(thruster);
            try
            {
                return thrusterDict[rotation];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogWarning($"thruster {thruster} with rotation {rotation} cannot find a corresponding thruster group");
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

        private int GetRotation(Thruster thruster)
        {
            return thruster.anchoredBlueprintBlock.blueprintBlock.Rotation;
        }
    }
}