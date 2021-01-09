using System.Linq;
using DG.Tweening;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Ships;
using Exa.Utils;
using Exa.VFX;
using UnityEngine;
using UnityEngine.VFX;

#pragma warning disable 649

namespace Exa.Grids.Blocks.Components
{
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData>
    {
        [Header("References")]
        [SerializeField] private Animator animator;
        [SerializeField] private Transform beamOrigin;
        [SerializeField] private GaussCannonArcs arcs;
        [SerializeField] private LineRenderer lineRenderer;

        public override void StartCharge() {
            base.StartCharge();
            animator.Play("Charge", 0, GetNormalizedChargeProgress());
            arcs.gameObject.SetActive(true);
        }

        public override void EndCharge() {
            if (charging) {
                animator.Play("CancelCharge", 0, 1f - GetNormalizedChargeProgress());
            }

            arcs.gameObject.SetActive(false);
            base.EndCharge();
        }

        public override void Fire() {
            var endPoint = HitScanFire(Data.Damage, Data.Range, beamOrigin);
            arcs.gameObject.SetActive(false);

            lineRenderer.SetPosition(0, beamOrigin.position);
            lineRenderer.SetPosition(1, endPoint);

            lineRenderer.gameObject.SetActive(true);
            lineRenderer.widthMultiplier = 1.5f;
            lineRenderer.DOWidthMultiplier(0f, 0.5f)
                .OnComplete(() => lineRenderer.gameObject.SetActive(false));
        }

        protected override void OnAdd() {
            base.OnAdd();
            var chargeSpeed = 1f / Data.chargeTime;
            animator.SetFloat("ChargeSpeed", chargeSpeed);
            animator.SetFloat("ChargeDecaySpeed", chargeSpeed * chargeDecaySpeed);

            arcs.RandomizeMaterials();
            arcs.ChargeTime = Data.chargeTime;
        }
    }
}