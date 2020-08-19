using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.Ships;
using Exa.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Ships
{
    public struct ThrusterArchetype
    {
        public ThrusterBehaviour thrusterBehaviour;
        public PowerConsumerBehaviour powerConsumerBehaviour;
    }

    public class ThrustVectors
    {
        private readonly BlockGrid blocks;
        private Dictionary<Vector2Int, ThrusterGroup> thrusterGroups;

        public ThrustVectors(BlockGrid blocks)
        {
            this.blocks = blocks;
            thrusterGroups = new Dictionary<Vector2Int, ThrusterGroup>
            {
                { Vector2Int.right, new ThrusterGroup()},
                { Vector2Int.down,  new ThrusterGroup()},
                { Vector2Int.left,  new ThrusterGroup()},
                { Vector2Int.up,    new ThrusterGroup()}
            };
        }

        public ThrusterGroup Right => thrusterGroups[Vector2Int.right];
        public ThrusterGroup Down => thrusterGroups[Vector2Int.down];
        public ThrusterGroup Left => thrusterGroups[Vector2Int.left];
        public ThrusterGroup Up => thrusterGroups[Vector2Int.up];

        public void Register(GameObject key, ThrusterArchetype thrusterArchetype)
        {
            SelectGroup(key).Register(key, thrusterArchetype);
        }

        public void Unregister(GameObject key)
        {
            SelectGroup(key).Unregister(key);
        }

        // TODO: use ship mass to calculate max thrust
        public Vector2 GetThrustCoefficient(Vector2 thrust, float deltaTime)
        {
            return new Vector2
            {
                x = SelectHorizontalGroup(thrust)   .GetThrustCoefficient(Mathf.Abs(thrust.x), deltaTime),
                y = SelectVerticalGroup(thrust)     .GetThrustCoefficient(Mathf.Abs(thrust.y), deltaTime)
            };
        }

        public float GetFireCoefficientConsumption(Vector2 thrust, Vector2 coefficient)
        {
            return 
                SelectHorizontalGroup(thrust)   .GetCoefficientConsumption(coefficient.x) +
                SelectVerticalGroup(thrust)     .GetCoefficientConsumption(coefficient.y);
        }

        public void Fire(Vector2 thrust)
        {
            throw new NotImplementedException();
        }

        private int RoundTo1(float value)
        {
            return value > 0 ? 1 : -1;
        }

        private ThrusterGroup SelectHorizontalGroup(Vector2 thrust)
        {
            return thrusterGroups[new Vector2Int(RoundTo1(thrust.x), 0)];
        }

        private ThrusterGroup SelectVerticalGroup(Vector2 thrust)
        {
            return thrusterGroups[new Vector2Int(0, RoundTo1(thrust.y))];
        }

        private ThrusterGroup SelectGroup(GameObject holder)
        {
            var rotation = holder.transform.localRotation;
            var angle = rotation.eulerAngles.z - 180f;
            var key = MathUtils.FromAngledMagnitude(1, angle);
            return thrusterGroups[key.ToVector2Int()];
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLineIndented($"Right :{Right}", tabs);
            sb.AppendLineIndented($"Down  :{Down}", tabs);
            sb.AppendLineIndented($"Left  :{Left}", tabs);
            sb.AppendLineIndented($"Up    :{Up}", tabs);
            return sb.ToString();
        }
    }
}