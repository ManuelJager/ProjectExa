using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Ships.Navigation
{
    public class ThrusterGroup : List<IThruster>
    {
        private Scalar _thrustModifier;
        private float _thrust = 0f;

        public float Thrust => _thrustModifier.GetValue(_thrust);

        public ThrusterGroup(Scalar thrustModifier)
        {
            this._thrustModifier = thrustModifier;
        }

        public new void Add(IThruster thruster)
        {
            base.Add(thruster);
            _thrust += thruster.Component.Data.thrust;
        }

        public new bool Remove(IThruster thruster)
        {
            var result = base.Remove(thruster);

            if (result)
            {
                _thrust -= thruster.Component.Data.thrust;
            }

            return result;
        }

        public void Fire(float force)
        {
            SetGraphics(force / Thrust);
        }

        public void SetGraphics(float strength)
        {
            foreach (var item in this)
            {
                item.Fire(strength);
            }
        }
    }
}