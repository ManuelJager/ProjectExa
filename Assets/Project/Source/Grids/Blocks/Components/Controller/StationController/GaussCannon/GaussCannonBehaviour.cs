using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData>
    {
        [Header("References")]
        [SerializeField] private Animator animator;

        public override void StartCharge() {
            base.StartCharge();
            animator.Play("Charge", 0, GetNormalizedChargeProgress());
#if UNITY_EDITOR
            UnityEditor.EditorGUIUtility.PingObject(turret);
#endif
        }

        public override void EndCharge() {
            if (charging) {
                animator.Play("CancelCharge", 0, 1f - GetNormalizedChargeProgress());
            }
            base.EndCharge();
        }

        public override void Fire() {
            Debug.Log("Fired");
        }

        protected override void OnAdd() {
            base.OnAdd();
            var chargeSpeed = 1f / Data.chargeTime;
            animator.SetFloat("ChargeSpeed", chargeSpeed);
            animator.SetFloat("ChargeDecaySpeed", chargeSpeed * chargeDecaySpeed);
        }
    }
}