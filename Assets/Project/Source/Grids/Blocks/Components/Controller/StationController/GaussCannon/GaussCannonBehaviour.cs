using DG.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blocks.Components
{
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData>
    {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Transform beamOrigin;
        [SerializeField] private LineRenderer lineRenderer;

        public override void StartCharge() {
            base.StartCharge();
            animator.Play("Charge", 0, GetNormalizedChargeProgress());
        }

        protected override void BlockUpdate() {
            base.BlockUpdate();
        }

        public override void EndCharge() {
            if (charging) {
                animator.Play("CancelCharge", 0, 1f - GetNormalizedChargeProgress());
            }
            base.EndCharge();
        }

        public override void Fire() {
            lineRenderer.SetPosition(0, beamOrigin.position);
            lineRenderer.SetPosition(1, beamOrigin.right * 10);

            lineRenderer.enabled = true;
            lineRenderer.widthMultiplier = 1f;
            lineRenderer.DOWidthMultiplier(0f, 0.5f)
                .OnComplete(() => lineRenderer.enabled = false);
        }

        protected override void OnAdd() {
            base.OnAdd();
            var chargeSpeed = 1f / Data.chargeTime;
            animator.SetFloat("ChargeSpeed", chargeSpeed);
            animator.SetFloat("ChargeDecaySpeed", chargeSpeed * chargeDecaySpeed);
        }
    }
}