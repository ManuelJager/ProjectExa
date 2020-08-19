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
        private ThrustVectors thrustVectors;
        private Rigidbody rb;
        private Transform transform;

        private TempValues tempValues;

        public ThrusterFireAction(Ship ship)
            : base(ship)
        {
            this.thrustVectors  = ship.blockGrid.ThrustVectors;
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
            var forceCoefficient = GetClampedCoefficient(localForce, deltaTime);

            tempValues = new TempValues
            {
                localForce = localForce,
                forceCoefficient = forceCoefficient,
                rotationAngle = rotationAngle
            };

            return thrustVectors.GetFireCoefficientConsumption(localForce, forceCoefficient);
        }

        public override void Update(float energyCoefficient, float deltaTime)
        {
            // Don't need to operate on a zero vecot
            if (rawForce == Vector2.zero) return;

            // Apply the normalization to the local acceleration
            var calculatedLocalForce = tempValues.localForce * tempValues.forceCoefficient * energyCoefficient;

            // Transform clamped local acceleration back to global acceleration
            var finalForce = MathUtils.Rotate(calculatedLocalForce, tempValues.rotationAngle);

            rb.AddForce(finalForce, ForceMode.Force);
        }

        private Vector2 GetClampedCoefficient(Vector2 localAcceleration, float deltaTime)
        {
            // Clamp the acceleration using the thrust vectors of the current ship
            var coefficient = thrustVectors.GetThrustCoefficient(localAcceleration, deltaTime);

            // Keep ratio to prevent drifting
            return MathUtils.ClampToLowestComponent(coefficient);
        }

        private struct TempValues
        {
            public Vector2 localForce;
            public Vector2 forceCoefficient;
            public float rotationAngle;
        }
    }
}