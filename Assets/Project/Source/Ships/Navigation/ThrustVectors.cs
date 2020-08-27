using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships.Navigations
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

        public void Fire(Vector2 force, float deltaTime)
        {
            SelectHorizontalGroup(force.x, false)   .Fire(Mathf.Abs(force.x),   deltaTime);
            SelectHorizontalGroup(force.x, true)    .Fire(0,                    deltaTime);
            SelectVerticalGroup(force.y, false)     .Fire(Mathf.Abs(force.y),   deltaTime);
            SelectVerticalGroup(force.y, true)      .Fire(0,                    deltaTime);
        }

        public Vector2 ClampForce(Vector2 localForce, float deltaTime)
        {
            var forceCoefficient = new Vector2
            {
                x = SelectHorizontalGroup(localForce.x).ClampThrustCoefficient(Mathf.Abs(localForce.x), deltaTime),
                y = SelectVerticalGroup(localForce.y).ClampThrustCoefficient(Mathf.Abs(localForce.y), deltaTime)
            };

            return MathUtils.ClampToLowestComponent(forceCoefficient) * localForce;
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

        private ThrusterGroup SelectHorizontalGroup(float x, bool revert = false) =>
            x > 0 ^ revert
                ? thrusterDict[0]
                : thrusterDict[2];

        private ThrusterGroup SelectVerticalGroup(float y, bool revert = false) =>
            y > 0 ^ revert
                ? thrusterDict[1]
                : thrusterDict[3];

        private int GetDirection(IThruster thruster)
        {
            return thruster.Component.block.anchoredBlueprintBlock.blueprintBlock.Direction;
        }
    }
}