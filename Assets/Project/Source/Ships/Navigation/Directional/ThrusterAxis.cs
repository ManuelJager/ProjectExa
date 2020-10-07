using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class ThrusterAxis
    {
        private readonly ThrusterGroup _positiveThrusterGroup;
        private readonly ThrusterGroup _negativeThrusterGroup;

        public ThrusterAxis(Scalar thrustModifier)
        {
            _positiveThrusterGroup = new ThrusterGroup(thrustModifier);
            _negativeThrusterGroup = new ThrusterGroup(thrustModifier);
        }

        public void Register(IThruster thruster, bool positiveComponent)
        {
            SelectGroup(positiveComponent).Add(thruster);
        }

        public void Unregister(IThruster thruster, bool positiveComponent)
        {
            SelectGroup(positiveComponent).Remove(thruster);
        }

        public float GetComponentThrust(bool positiveComponent)
        {
            return SelectGroup(positiveComponent).Thrust;
        }

        public void Fire(float velocity)
        {
            var component = velocity > 0f;
            SelectGroup(component).Fire(Mathf.Abs(velocity));
            SelectGroup(!component).Fire(0f);
        }

        public float Clamp(float directionForce)
        {
            var positive = directionForce > 0f;
            var maxForceDelta = SelectGroup(positive).Thrust;
            return positive ? maxForceDelta : -maxForceDelta;
        }

        public void SetGraphics(float scalar)
        {
            var component = scalar > 0f;
            SelectGroup(component).SetGraphics(Mathf.Abs(scalar));
            SelectGroup(!component).SetGraphics(0f);
        }

        private ThrusterGroup SelectGroup(bool positiveComponent)
        {
            return positiveComponent
                ? _positiveThrusterGroup
                : _negativeThrusterGroup;
        }
    }
}