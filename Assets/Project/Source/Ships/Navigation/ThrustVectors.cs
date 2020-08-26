using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    // TODO: Clamp the requested thrust vector to what the ship can output
    public class ThrustVectors
    {
        private Dictionary<int, ThrusterGroup> thrusterDict;

        public ThrustVectors(float directionalThrust)
        {
            thrusterDict = new Dictionary<int, ThrusterGroup>
            {
                { 0, new ThrusterGroup(directionalThrust) },
                { 1, new ThrusterGroup(directionalThrust) },
                { 2, new ThrusterGroup(directionalThrust) },
                { 3, new ThrusterGroup(directionalThrust) }
            };
        }

        public void Register(IThruster thruster)
        {
            SelectGroup(thruster)?.Add(thruster);
        }

        public void Unregister(IThruster thruster)
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

        private ThrusterGroup SelectGroup(IThruster thruster)
        {
            var rotation = GetDirection(thruster);
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

        private int GetDirection(IThruster thruster)
        {
            return thruster.Component.block.anchoredBlueprintBlock.blueprintBlock.Direction;
        }
    }
}