using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public abstract class AutoFireTurret<T> : TurretBehaviour<T>, IAutoFireTurret
        where T : struct, ITurretValues
    {
        public bool AutoFireEnabled { get; set; } = true;

        protected override void BlockUpdate() {
            timeSinceFire += Time.deltaTime;
            var targetAngleDelta = TryRotateTowardsTarget();
            if (targetAngleDelta != null) {
                AttemptFire(targetAngleDelta.Value);
            }
        }

        protected virtual void AttemptFire(float targetAngleDelta) {
            if (AutoFire && targetAngleDelta < 0.5f && timeSinceFire > data.FiringRate) {
                timeSinceFire -= data.FiringRate;
                Fire();
            }
        }
    }
}