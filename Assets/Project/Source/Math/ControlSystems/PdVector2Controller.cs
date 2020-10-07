using UnityEngine;

namespace Exa.Math.ControlSystems
{
    public class PdVector2Controller
    {
        private float _proportional;
        private float _maxVel;
        private float _derivitive;

        public float Proportional
        {
            get => _proportional;
            set => _proportional = value;
        }

        public float Derivitive
        {
            get => _derivitive;
            set => _derivitive = value;
        }

        public float MaxVel 
        { 
            get => _maxVel; 
            set => _maxVel = value; 
        }

        public PdVector2Controller(float proportional, float derivitive, float maxVel)
        {
            this._proportional = proportional;
            this._derivitive = derivitive;
            this._maxVel = maxVel;
        }

        public void SetSettings(PdSettings pdSettings)
        {
            this.Proportional = pdSettings.proportional;
            this.Derivitive = pdSettings.derivitive;
        }

        public Vector2 CalculateRequiredVelocity(Vector2 currentPos, Vector2 targetPos, Vector2 currentVelocity)
        {
            var dist = targetPos - currentPos;
            var targetVel = Vector2.ClampMagnitude(_proportional * dist, _maxVel);
            var error = targetVel - currentVelocity;
            return _derivitive * error;
        }
    }
}