using System;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class ChargeableTurretBehaviour<T> : TurretBehaviour<T>
        where T : struct, IChargeableTurretValues
    {
        protected bool charging;
        protected float chargeTime;

        [Header("Settings")]
        [SerializeField] protected float chargeDecaySpeed = 4f;

        public virtual void StartCharge() {
            charging = true;
            chargeTime = 0f;
        }

        public virtual void EndCharge() {
            charging = false;
        }

        protected override void BlockUpdate() {
            base.BlockUpdate();
            if (charging) {
                chargeTime += Time.deltaTime;
                if (chargeTime > Data.ChargeTime) {
                    charging = false;
                    Fire();
                    chargeTime = 0f;
                }
            }
            else {
                chargeTime -= Time.deltaTime * chargeDecaySpeed;
            }

            if (chargeTime < 0f) {
                chargeTime = 0f;
            }
        }

        protected float GetNormalizedChargeProgress() {
            return chargeTime == 0f ? 0f : chargeTime / Data.ChargeTime;
        }
    }
}