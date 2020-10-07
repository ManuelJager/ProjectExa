using System;
using UnityEngine;

namespace Exa.Math.ControlSystems
{
    public class PidController
    {
        private const float MaxOutput = 1000.0f;

        private float _calculatedIntegralMax;
        private float _calculatedIntegral;

        private float _proportional;
        private float _integral;
        private float _derivitive;

        public PidController(float proportional, float integral, float derivitive)
        {
            Proportional = proportional;
            Integral = integral;
            Derivitive = derivitive;

            this._calculatedIntegralMax = MaxOutput / Integral;
        }

        public float Proportional
        {
            get => _proportional;
            set
            {
                EnsureNonNegative(_proportional, "proportional");
                _proportional = value;
            }
        }

        public float Integral
        {
            get => _integral;
            set
            {
                EnsureNonNegative(_integral, "integral");

                _integral = value;

                this._calculatedIntegralMax = MaxOutput / Integral;
                this._calculatedIntegral = Mathf.Clamp(this._calculatedIntegral, -this._calculatedIntegralMax, this._calculatedIntegralMax);
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
            get => _derivitive;
            set
            {
                EnsureNonNegative(value, "Derivative");
                this._derivitive = value;
            }
        }

        public float ComputeOutput(float error, float delta, float deltaTime)
        {
            this._calculatedIntegral += (error * deltaTime);
            this._calculatedIntegral = Mathf.Clamp(this._calculatedIntegral, -this._calculatedIntegralMax, this._calculatedIntegralMax);

            float derivative = delta / deltaTime;
            float output = (Proportional * error) + (Integral * this._calculatedIntegral) + (Derivitive * derivative);

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