using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class ThrusterGroup
    {
        private List<ThrusterBehaviour> thrusters;
        private float totalThrust;
        private float totalConsumption;

        public ThrusterGroup()
        {
            thrusters = new List<ThrusterBehaviour>();
        }

        public void Register(ThrusterBehaviour thruster, float thrust, float consumption)
        {
            thrusters.Add(thruster);
            totalThrust += thrust * 1000;
            totalConsumption += consumption;
        }

        public void Unregister(ThrusterBehaviour thruster, float thrust, float consumption)
        {
            thrusters.Remove(thruster);
            totalThrust -= thrust * 1000;
            totalConsumption -= consumption;
        }

        public float GetThrustCoefficient(float thrust, float deltaTime)
        {
            return Mathf.Clamp(thrust, 0f, totalThrust * deltaTime) / thrust;
        }

        public float GetCoefficientConsumption(float coefficient)
        {
            return totalConsumption * coefficient;
        }

        public void Fire(float coefficient)
        {

        }

        public override string ToString()
        {
            return $"{totalThrust}";
        }
    }
}