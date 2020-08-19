using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships
{
    public class ThrusterGroup
    {
        private Dictionary<GameObject, ThrusterArchetype> thrusters;
        private float totalThrust;
        private float totalConsumption;

        public ThrusterGroup()
        {
            thrusters = new Dictionary<GameObject, ThrusterArchetype>();
        }

        public void Register(GameObject key, ThrusterArchetype archetype)
        {
            thrusters.Add(key, archetype);
            totalThrust += archetype.thrusterBehaviour.Data.thrust * 1000;
            totalConsumption += archetype.powerConsumerBehaviour.Data.powerConsumption;
        }

        public void Unregister(GameObject key)
        {
            var archetype = thrusters[key];
            totalThrust -= archetype.thrusterBehaviour.Data.thrust * 1000;
            totalConsumption -= archetype.powerConsumerBehaviour.Data.powerConsumption;
            thrusters.Remove(key);
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