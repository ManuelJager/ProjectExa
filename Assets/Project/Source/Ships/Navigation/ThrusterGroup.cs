using Exa.Data;
using Exa.Grids.Blocks.BlockTypes;
using System.Collections.Generic;
using Exa.Grids.Blocks.Components;

namespace Exa.Ships.Navigation
{
    public class ThrusterGroup : List<ThrusterBehaviour>
    {
        private Scalar thrustModifier;
        private float thrust = 0f;

        public float Thrust => thrustModifier.GetValue(thrust);

        public ThrusterGroup(Scalar thrustModifier) {
            this.thrustModifier = thrustModifier;
        }

        public new void Add(ThrusterBehaviour thruster) {
            base.Add(thruster);
            thrust += thruster.Data.thrust;
        }

        public new bool Remove(ThrusterBehaviour thruster) {
            var result = base.Remove(thruster);

            if (result) {
                thrust -= thruster.Data.thrust;
            }

            return result;
        }

        public void Fire(float force) {
            SetGraphics(force / Thrust);
        }

        public void SetGraphics(float strength) {
            foreach (var item in this) {
                item.Fire(strength);
            }
        }
    }
}