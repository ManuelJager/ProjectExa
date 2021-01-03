using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class ChargeableTurretBehaviour<T> : TurretBehaviour<T>
        where T : struct, IChargeableTurretValues
    {
        private bool charging;
        private float chargeTime;

        public void StartCharge() {
            charging = true;
            chargeTime = 0f;
        }

        public void EndCharge() {
            charging = false;
        }

        protected override void BlockUpdate() {
            base.BlockUpdate();
            if (charging) {
                chargeTime += Time.deltaTime;
                if (chargeTime > Data.ChargeTime) {
                    Fire();
                    chargeTime = 0f;
                }
            }
        }
    }
}