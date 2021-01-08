using System.Linq;
using DG.Tweening;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
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

        public override void EndCharge() {
            if (charging) {
                animator.Play("CancelCharge", 0, 1f - GetNormalizedChargeProgress());
            }
            base.EndCharge();
        }

        public override void Fire() {
            var endPoint = HitScanFire(Data.Damage, Data.Range, beamOrigin);

            lineRenderer.SetPosition(0, beamOrigin.position);
            lineRenderer.SetPosition(1, endPoint);

            lineRenderer.enabled = true;
            lineRenderer.widthMultiplier = 1.5f;
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