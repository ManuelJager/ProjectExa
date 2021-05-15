using System;
using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targeting;
using Exa.Types.Generics;
using Exa.Utils;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

        protected RotationResult? TryRotateTowardsTarget() {
            // Dereference target if it isn't valid anymore
            if (!Target?.GetTargetValid() ?? false) {
                Target = null;
            }

            var result = RotateTowards(SelectTargetAngle());
            DoRotate(result);
            return result;
        }

        protected virtual float SelectTargetAngle() {
            if (Target == null) {
                return GetDefaultAngle();
            }
            
            var currentPosition = transform.position.ToVector2();
            var difference = Target.GetPosition(currentPosition) - currentPosition;
            return difference.GetAngle(); 
        }

        protected float GetDefaultAngle() {
            return Parent.Transform.right.ToVector2().GetAngle();
        }

        protected virtual RotationResult RotateTowards(float targetAngle) {
            var currentAngle = turret.rotation.eulerAngles.z;
            var deltaAngle = GetDeltaAngleTowards(currentAngle, targetAngle);

            if (deltaAngle == 0f) {
                return new RotationResult(0f, 0f, currentAngle);
            }

            var maxAngles = Data.TurningRate * Time.deltaTime;
            var offset = deltaAngle > 0f
                ? Mathf.Min(maxAngles, deltaAngle)
                : Mathf.Max(-maxAngles, deltaAngle);

            return new RotationResult {
                deltaToTarget = Mathf.DeltaAngle(currentAngle + offset, targetAngle),
                frameRotation = offset,
                endRotation = currentAngle + offset
            };
        }

        protected virtual float GetDeltaAngleTowards(float currentAngle, float targetAngle) {
            if (Data.TurretArc == 360f) {
                return Mathf.DeltaAngle(currentAngle, targetAngle);
            }

            var (min, max) = Data.GetTurretArcMinMax().AsTuple();

            throw new NotImplementedException();
        }

        protected virtual void DoRotate(RotationResult result) {
            turret.rotation = Quaternion.Euler(0, 0, result.endRotation);
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

    public struct RotationResult
    {
        public float deltaToTarget;
        public float frameRotation;
        public float endRotation;

        public RotationResult(float deltaToTarget, float frameRotation, float endRotation) {
            this.deltaToTarget = deltaToTarget;
            this.frameRotation = frameRotation;
            this.endRotation = endRotation;
        }
    }
}