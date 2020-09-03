using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class ThrusterGroup : List<IThruster>
    {
        private float directionalThrust;
        private Percentage thrustModifier = new Percentage(1);

        private float Thrust
        {
            get => thrustModifier.GetValue(directionalThrust);
        }

        public ThrusterGroup(float directionalThrust)
        {
            this.directionalThrust = directionalThrust;
        }

        public new void Add(IThruster thruster)
        {
            base.Add(thruster);
            thrustModifier += thruster.Component.Data.thrust;
        }

        public new bool Remove(IThruster thruster)
        {
            var result = base.Remove(thruster);

            if (result)
            {
                thrustModifier -= thruster.Component.Data.thrust;
            }

            return result;
        }

        public void Fire(float force, float deltaTime)
        {
            var strength = Mathf.Clamp01(force / (Thrust * deltaTime));
            SetFireStrength(strength);
        }

        public void SetFireStrength(float strength)
        {
            foreach (var item in this)
            {
                item.Fire(strength);
            }
        }

        public float ClampThrustCoefficient(float force, float deltaTime)
        {
            return Mathf.Clamp01(Thrust * deltaTime / force);
        }
    }
}