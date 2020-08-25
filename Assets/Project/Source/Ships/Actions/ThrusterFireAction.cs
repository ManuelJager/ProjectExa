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
        private Rigidbody rb;
        private Transform transform;

        private TempValues tempValues;

        public ThrusterFireAction(Ship ship)
            : base(ship)
        {
            this.rb             = ship.navigation.rb;
            this.transform      = ship.transform;
        }

        public void Update(Vector2 rawForce)
        {
            this.rawForce = rawForce;
        }

        public override float CalculateConsumption(float deltaTime)
        {
            if (rawForce == Vector2.zero) return 0f;

            // Get the angle the ship is currently facing
            var rotationAngle = transform.localRotation.eulerAngles.z;

            // Rotate the acceleration needed to local space
            var localForce = MathUtils.Rotate(rawForce, -rotationAngle);

            tempValues = new TempValues
            {
                localForce = localForce,
                rotationAngle = rotationAngle
            };

            // TODO: Replace this with an actual value
            return 1f;
        }

        public override void Update(float energyCoefficient, float deltaTime)
        {
            // Don't need to operate on a zero vector
            if (rawForce == Vector2.zero)
            {
                ship.navigation.thrustVectors.Fire(Vector2.zero);
                return;
            }

            //Debug.Log(tempValues.localForce);
            //Debug.Log(tempValues.forceCoefficient);
            //Debug.Log(energyCoefficient);

            // Apply the normalization to the local acceleration
            var calculatedLocalForce = tempValues.localForce * energyCoefficient;

            // Transform clamped local acceleration back to global acceleration
            var finalForce = MathUtils.Rotate(calculatedLocalForce, tempValues.rotationAngle);
            ship.navigation.thrustVectors.Fire(calculatedLocalForce);

            rb.AddForce(finalForce, ForceMode.Force);
        }

        private struct TempValues
        {
            public Vector2 localForce;
            public float rotationAngle;
        }
    }
}