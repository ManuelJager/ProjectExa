using Exa.Debugging;
using Exa.Math;
using System;
using UnityEngine;

namespace Exa.Ships
{
    /// <summary>
    /// Class that handles the firing of thrusters
    /// </summary>
    public class ThrusterFireAction : ShipAction
    {
        private Vector2 rawForce;
        private TempValues tempValues;

        public void Update(Vector2 rawForce)
        {
            this.rawForce = rawForce;
        }

        public ThrusterFireAction(Ship ship)
            : base(ship)
        {
        }

        public override float CalculateConsumption(float deltaTime)
        {
            if (rawForce == Vector2.zero) return 0f;

            // Get the angle the ship is currently facing
            var rotationAngle = ship.transform.localRotation.eulerAngles.z;

            // Rotate the acceleration needed to local space
            var localForce = MathUtils.Rotate(rawForce, -rotationAngle);

            tempValues = new TempValues
            {
                localForce = localForce,
                rotationAngle = rotationAngle
            };

            return 0f;
        }

        public override void Update(float energyCoefficient, float deltaTime)
        {
            // Don't need to operate on a zero vector
            if (rawForce == Vector2.zero)
            {
                ship.navigation.thrustVectors.Fire(Vector2.zero, deltaTime);
                return;
            }

            // Apply the normalization to the local acceleration
            var calculatedLocalForce = ship.navigation.thrustVectors.ClampForce(tempValues.localForce, deltaTime);

            // Transform clamped local acceleration back to global acceleration
            var finalForce = MathUtils.Rotate(calculatedLocalForce, tempValues.rotationAngle);
            ship.navigation.thrustVectors.Fire(calculatedLocalForce, deltaTime);

            ship.rb.AddForce(finalForce, ForceMode2D.Force);
        }

        private struct TempValues
        {
            public Vector2 localForce;
            public float rotationAngle;
        }
    }
}