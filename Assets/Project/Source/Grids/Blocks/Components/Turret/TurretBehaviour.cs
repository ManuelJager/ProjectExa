using Exa.Math;
using Exa.Ships.Targeting;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.Components
{
    public abstract class TurretBehaviour<T> : BlockBehaviour<T>
        where T : struct, ITurretValues
    {
        [Header("References")] [SerializeField]
        private Transform turret;

        private float timeSinceFire;

        public ITarget Target { get; set; }

        public void Update() {
            timeSinceFire += Time.deltaTime;

            if (Target == null) {
                var shipAngle = ship.transform.right.ToVector2().GetAngle();
                RotateTowards(shipAngle);
                return;
            }

            // Rotate the turret to the target
            var currentPosition = transform.position.ToVector2();
            var difference = Target.GetPosition(currentPosition) - currentPosition;
            var targetAngle = difference.GetAngle();
            RotateTowards(targetAngle);

            var currentAngle = turret.rotation.eulerAngles.z;
            if (WithinFiringFrustum(currentAngle, targetAngle) && timeSinceFire > data.FiringRate) {
                timeSinceFire = 0f;
                Fire();
            }
        }

        // TODO: Actually rotate using the rotation speed
        public void RotateTowards(float angle) {
            turret.rotation = Quaternion.Euler(0, 0, angle);
        }

        public abstract void Fire();

        private bool WithinFiringFrustum(float currentAngle, float targetAngle) {
            return Mathf.DeltaAngle(currentAngle, targetAngle) < 2.5f;
        }
    }
}