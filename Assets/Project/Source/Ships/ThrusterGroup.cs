using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class ThrusterGroup
    {
        private List<Thruster> thrusters;
        private float totalThrust;

        public ThrusterGroup()
        {
            thrusters = new List<Thruster>();
        }

        public void Register(Thruster thruster)
        {
            thrusters.Add(thruster);
            totalThrust += (thruster as IThruster).Component.Data.newtonThrust * 1000;
        }

        public void Unregister(Thruster thruster)
        {
            thrusters.Remove(thruster);
            totalThrust -= (thruster as IThruster).Component.Data.newtonThrust * 1000;
        }

        /// <summary>
        /// Fire the thrusters
        /// </summary>
        /// <returns>Force this group should apply</returns>
        public float ClampThrust(float thrust, float deltaTime)
        {
            return Mathf.Clamp(thrust, 0f, totalThrust * deltaTime);
        }

        public override string ToString()
        {
            return $"{totalThrust}";
        }
    }
}