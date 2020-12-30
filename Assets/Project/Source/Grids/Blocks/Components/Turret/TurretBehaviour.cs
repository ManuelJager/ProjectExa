using Exa.Math;
using Exa.Ships.Targeting;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components
{
    public abstract class TurretBehaviour<T> : BlockBehaviour<T>
        where T : struct, ITurretValues
    {
        [Header("References")]
        [SerializeField] private Transform turret;

        protected float timeSinceFire;

        public IWeaponTarget Target { get; set; }
        public virtual bool AutoFire => true;

        protected override void BlockUpdate() {
            timeSinceFire += Time.deltaTime;
            TryRotateTowardsTarget();
        }

        protected float? TryRotateTowardsTarget() {
            if (!Target?.GetTargetValid() ?? false) {
                Target = null;
            }

            if (Target == null) {
                RotateTowards(GetDefaultAngle());
                return null;
            }

            // Rotate the turret to the target
            var currentPosition = transform.position.ToVector2();
            var difference = Target.GetPosition(currentPosition) - currentPosition;
            var targetAngle = difference.GetAngle();
            return RotateTowards(targetAngle);
        }

        protected float GetDefaultAngle() {
            return Parent.Transform.right.ToVector2().GetAngle();
        }

        // TODO: Actually rotate using the rotation speed
        protected float RotateTowards(float targetAngle) {
            turret.rotation = Quaternion.Euler(0, 0, targetAngle);
            var currentAngle = turret.rotation.eulerAngles.z;
            return Mathf.DeltaAngle(currentAngle, targetAngle);
        }

        public abstract void Fire();
    }
}