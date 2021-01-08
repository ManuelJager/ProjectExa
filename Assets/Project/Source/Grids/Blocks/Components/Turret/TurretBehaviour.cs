using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targeting;
using Exa.Utils;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components
{
    public abstract class TurretBehaviour<T> : BlockBehaviour<T>
        where T : struct, ITurretValues
    {
        [Header("References")]
        [SerializeField] protected Transform turret;

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

        protected Vector3 HitScanFire(float damage, float maxDistance, Transform firingPoint) {
            var enemyMask = (~Parent.BlockContext).GetBlockMask();
            var hits = PhysicsExtensions.RaycastAll(firingPoint.position, firingPoint.right, maxDistance, enemyMask);

            var lastHitPosition = new Vector2();

            using var hitsEnumerator = hits.GetEnumerator();

            while (damage > 0f && hitsEnumerator.MoveNext(out var hit)) {
                lastHitPosition = hit.hit.point;
                var damageInstance = hit.block.PhysicalBehaviour.AbsorbDamage(Parent, damage);
                damage -= damageInstance.absorbedDamage;
            }

            if (damage > 0f) {
                return firingPoint.right * maxDistance;
            }

            return lastHitPosition;
        }
    }
}