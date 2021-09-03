using Exa.Data;
using UnityEngine;

namespace Exa.Grids.Blocks.Components {
    public abstract class ChargeableTurretBehaviour<T> : TurretBehaviour<T>, IChargeableTurretBehaviour
        where T : struct, IChargeableTurretValues {
        [Header("Settings")]
        [SerializeField] protected float chargeDecaySpeed = 4f;
        [SerializeField] protected float coolingDownChargeDecaySpeed = 12f;
        
        protected float chargeTime;
        protected bool charging;
        protected float remainingCooldownTime;

        protected abstract bool CanResumeCharge { get; }
        protected abstract float GetCooldownTime { get; }

        protected bool IsCoolingDown {
            get => remainingCooldownTime > 0f;
        }

        public override void Fire() {
            remainingCooldownTime = GetCooldownTime;
        }

        public virtual void StartCharge() {
            if (IsCoolingDown) {
                return;
            }
            
            charging = true;

            if (!CanResumeCharge) {
                chargeTime = 0f;
            }
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
                    chargeTime = Data.ChargeTime;
                    Fire();
                }
            } else if (IsCoolingDown) {
                chargeTime -= Time.deltaTime * coolingDownChargeDecaySpeed;
                remainingCooldownTime -= Time.deltaTime;
            } else {
                chargeTime -= Time.deltaTime * chargeDecaySpeed;
            }

            if (chargeTime < 0f) {
                chargeTime = 0f;
            }
        }

        protected Scalar GetNormalizedChargeProgress() {
            // Get the charge 
            return chargeTime == 0f ? 0f : chargeTime / Data.ChargeTime;
        }
    }
}