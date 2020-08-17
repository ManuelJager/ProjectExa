using Exa.Grids.Blocks.BlockTypes;
using Exa.Math;
using Exa.Ships;
using Exa.Utils;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Exa.Grids
{
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

        public void Register(Thruster thruster)
        {
            SelectGroup(thruster).Register(thruster);
        }

        public void Unregister(Thruster thruster)
        {
            SelectGroup(thruster).Unregister(thruster);
        }

        // TODO: make this less ugly, also use ship mass to calculate max thrust
        public void ClampThrustVector(ref Vector2 vectorThrust, float deltaTime)
        {
            if (vectorThrust.x > 0)
            {
                vectorThrust.x =    Right.ClampThrust(vectorThrust.x, deltaTime);
            }

            if (vectorThrust.y < 0)
            {
                vectorThrust.y = -  Down.ClampThrust(-vectorThrust.y, deltaTime);
            }

            if (vectorThrust.x < 0)
            {
                vectorThrust.x = -  Left.ClampThrust(-vectorThrust.x, deltaTime);
            }

            if (vectorThrust.y > 0)
            {
                vectorThrust.y =    Up.ClampThrust(vectorThrust.y, deltaTime);
            }
        }

        private ThrusterGroup SelectGroup(Thruster thruster)
        {
            var rotation = thruster.transform.localRotation;
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