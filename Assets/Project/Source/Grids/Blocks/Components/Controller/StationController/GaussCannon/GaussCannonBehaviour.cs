using DG.Tweening;
using Exa.Audio;
using Exa.Math;
using Exa.Utils;
using Exa.VFX;
using UnityEngine;
using UnityEngine.Serialization;

#pragma warning disable 649

namespace Exa.Grids.Blocks.Components {
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData> {
        private static readonly int Charge = Animator.StringToHash("Charge");
        private static readonly int CancelCharge = Animator.StringToHash("CancelCharge");
        private static readonly int ChargeDecaySpeed = Animator.StringToHash("ChargeDecaySpeed");
        private static readonly int ChargeSpeed = Animator.StringToHash("ChargeSpeed");
       
        [Header("References")]
        [SerializeField] private Animator coilAnimator;
        [SerializeField] private Animator gunOverlayAnimator;
        [SerializeField] private LocalAudioPlayerProxy audioPlayer;
        [SerializeField] private Transform beamOrigin;
        [SerializeField] private GaussCannonArcs arcs;
        [SerializeField] private LineRenderer lineRenderer;

        public override bool CanResumeCharge {
            get => true;
        }

        public override void StartCharge() {
            base.StartCharge();

            var pos = GetNormalizedChargeProgress();
            coilAnimator.Play(Charge, 0, pos);
            gunOverlayAnimator.Play(Charge, 0, pos);
        }

        public override void EndCharge() {
            if (charging) {
                var pos = 1f - GetNormalizedChargeProgress();
                coilAnimator.Play(CancelCharge, 0, pos);
                gunOverlayAnimator.Play(CancelCharge, 0, pos);
            }

            base.EndCharge();
        }

        protected override void BlockUpdate() {
            var prevChargeTime = chargeTime;
            
            base.BlockUpdate();

            if (prevChargeTime != chargeTime) {
                arcs.SetChargeProgress(GetNormalizedChargeProgress(), charging);
            } 
        }

        public override void Fire() {
            arcs.Reset();
            
            var endPoint = HitScanFire(Data.damage, Data.Range, beamOrigin);

            lineRenderer.SetPosition(0, beamOrigin.position.SetZ(-0.2f));
            lineRenderer.SetPosition(1, endPoint.SetZ(-0.2f));

            lineRenderer.gameObject.SetActive(true);
            lineRenderer.widthMultiplier = 1.5f;
            
            lineRenderer.DOWidthMultiplier(0.5f, 0.5f)
                .OnComplete(() => lineRenderer.gameObject.SetActive(false));
        }

        protected override void OnAdd() {
            base.OnAdd();
            
            var chargeSpeed = 1f / Data.chargeTime;
            coilAnimator.SetFloat(ChargeSpeed, chargeSpeed);
            coilAnimator.SetFloat(ChargeDecaySpeed, chargeSpeed * chargeDecaySpeed);
            gunOverlayAnimator.SetFloat(ChargeSpeed, chargeSpeed);
            gunOverlayAnimator.SetFloat(ChargeDecaySpeed, chargeSpeed * chargeDecaySpeed);

            arcs.RandomizeMaterials();
        }
    }
}