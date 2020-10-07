using System;
using UnityEngine;

namespace Exa.Math.ControlSystems
{
    public class PidController
    {
        private const float MaxOutput = 1000.0f;

        private float calculatedIntegralMax;
        private float calculatedIntegral;

        private float proportional;
        private float integral;
        private float derivitive;

        public PidController(float proportional, float integral, float derivitive)
        {
            Proportional = proportional;
            Integral = integral;
            Derivitive = derivitive;

            this.calculatedIntegralMax = MaxOutput / Integral;
        }

        public float Proportional
        {
            get => proportional;
            set
            {
                EnsureNonNegative(proportional, "proportional");
                proportional = value;
            }
        }

        public float Integral
        {
            get => integral;
            set
            {
                EnsureNonNegative(integral, "integral");

                integral = value;

                this.calculatedIntegralMax = MaxOutput / Integral;
                this.calculatedIntegral = Mathf.Clamp(this.calculatedIntegral, -this.calculatedIntegralMax, this.calculatedIntegralMax);
            }
        }

        /// <summary>
        ///     Gets or sets the derivative gain.
        /// </summary>
        /// <value>
        ///     The derivative gain.
        /// </value>
        public float Derivitive
        {
            get => derivitive;
            set
            {
                EnsureNonNegative(value, "Derivative");
                this.derivitive = value;
            }
        }

        public float ComputeOutput(float error, float delta, float deltaTime)
        {
            this.calculatedIntegral += (error * deltaTime);
            this.calculatedIntegral = Mathf.Clamp(this.calculatedIntegral, -this.calculatedIntegralMax, this.calculatedIntegralMax);

            float derivative = delta / deltaTime;
            float output = (Proportional * error) + (Integral * this.calculatedIntegral) + (Derivitive * derivative);

            output = Mathf.Clamp(output, -MaxOutput, MaxOutput);

            return output;
        }

        private void EnsureNonNegative(float value, string paramName)
        {
            if (value < 0f)
            {
                throw new ArgumentOutOfRangeException(paramName, "param must be a non-negative number");
            }
        }
    }
}