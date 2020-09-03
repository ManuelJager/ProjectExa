using UnityEngine;

namespace Exa.Math.ControlSystems
{
    public class PdVector2Controller
    {
        private float proportional;
        private float maxVel;
        private float derivitive;

        public float Proportional
        {
            get => proportional;
            set => proportional = value;
        }

        public float Derivitive
        {
            get => derivitive;
            set => derivitive = value;
        }

        public float MaxVel 
        { 
            get => maxVel; 
            set => maxVel = value; 
        }

        public PdVector2Controller(float proportional, float derivitive, float maxVel)
        {
            this.proportional = proportional;
            this.derivitive = derivitive;
            this.maxVel = maxVel;
        }

        public void SetSettings(PdSettings pdSettings)
        {
            this.Proportional = pdSettings.proportional;
            this.Derivitive = pdSettings.derivitive;
        }

        public Vector2 CalculateRequiredVelocity(Vector2 currentPos, Vector2 targetPos, Vector2 currentVelocity)
        {
            var dist = targetPos - currentPos;
            var targetVel = Vector2.ClampMagnitude(proportional * dist, maxVel);
            var error = targetVel - currentVelocity;
            return derivitive * error;
        }
    }
}