using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targeting;
using Exa.Utils;
using UnityEngine;

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

        protected float RotateTowards(float targetAngle) {
            var currentAngle = turret.rotation.eulerAngles.z;
            var deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);

            if (deltaAngle == 0f) {
                return 0f;
            }

            var maxAngles = Data.TurningRate * Time.deltaTime;
            var offset = deltaAngle > 0f
                ? Mathf.Min(maxAngles, deltaAngle)
                : Mathf.Max(-maxAngles, deltaAngle);

            turret.rotation = Quaternion.Euler(0, 0, currentAngle + offset);

            return Mathf.DeltaAngle(turret.rotation.eulerAngles.z, targetAngle);
        }

        public abstract void Fire();

        protected Vector3 HitScanFire(float damage, float maxDistance, Transform firingPoint) {
            var enemyMask = (~Parent.BlockContext).GetBlockMask();
            var hits = PhysicsExtensions.RaycastAll(firingPoint.position, firingPoint.right, maxDistance, enemyMask);

            using var hitsEnumerator = hits.GetEnumerator();

            var lastHitPosition = new Vector2();

            while (damage > 0f && hitsEnumerator.MoveNext(out var hit)) {
                lastHitPosition = hit.hit.point;
                var damageInstance = hit.block.PhysicalBehaviour.AbsorbDamage(Parent, damage);
                damage -= damageInstance.absorbedDamage;
            }

            return damage > 0f 
                ? firingPoint.right * maxDistance 
                : lastHitPosition.ToVector3();
        }
    }
}