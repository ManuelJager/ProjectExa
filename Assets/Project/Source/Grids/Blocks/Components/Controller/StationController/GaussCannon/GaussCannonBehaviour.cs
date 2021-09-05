using System;
using DG.Tweening;
using Exa.Audio;
using Exa.Math;
using Exa.Types.Generics;
using Exa.UI.Cursor;
using Exa.UI.Tweening;
using Exa.Utils;
using Exa.VFX;
using UnityEngine;

#pragma warning disable 649

namespace Exa.Grids.Blocks.Components {
    public class GaussCannonBehaviour : ChargeableTurretBehaviour<GaussCannonData> {
        private static readonly int Charge = Animator.StringToHash("Charge");
        private static readonly int CancelCharge = Animator.StringToHash("CancelCharge");
        private static readonly int ChargeDecaySpeed = Animator.StringToHash("ChargeDecaySpeed");
        private static readonly int ChargeSpeed = Animator.StringToHash("ChargeSpeed");

        public static readonly int StepCount = 3;
        public static readonly float StepSize = 1f / 3f;
       
        [Header("References")]
        [SerializeField] private Animator coilAnimator;
        [SerializeField] private Animator gunOverlayAnimator;
        [SerializeField] private Transform beamOrigin;
        [SerializeField] private GaussCannonArcs arcs;
        [SerializeField] private LineRenderer lineRenderer;
        
        [Header("Audio")]
        [SerializeField] private LocalAudioPlayerProxy audioPlayer;
        [SerializeField] private Sound charge;
        [SerializeField] private Sound coilCharge1;
        [SerializeField] private Sound coilCharge2;
        [SerializeField] private Sound coilCharge3;
        [SerializeField] private Sound electricityLoop;
        [SerializeField] private Sound fire;
        [SerializeField] private Sound windDown;

        [Header("Settings")]
        [SerializeField] private ExaEase progressToElectricityVolume;
        [SerializeField] private ValueOverride<GaussCannonCursorFacade> cursorFacade;
        
        private SoundHandle fireSoundHandle;
        private SoundHandle electricityLoopHandle;
        
        private int prevCoilStep = -1;

        protected override bool CanResumeCharge {
            get => true;
        }

        protected override float GetCooldownTime {
            get => data.chargeTime * 2f;
        }

        private void Awake() {
            cursorFacade.Value.Init(S.UI.MouseCursor.VirtualMouseCursor, this);
        }

        public override void StartCharge() {
            if (IsCoolingDown) {
                return;
            }
            
            base.StartCharge();

            if (chargeTime == 0f) {
                S.UI.MouseCursor.CurrentCursor.CursorFacades?.Add(cursorFacade);
            }
            
            var pos = GetNormalizedChargeProgress();
            coilAnimator.Play(Charge, 0, pos);
            gunOverlayAnimator.Play(Charge, 0, pos);
            
            fireSoundHandle = audioPlayer.Play(charge, pos);
            electricityLoopHandle = audioPlayer.Play(electricityLoop, pos);
        }

        public override void EndCharge() {
            if (IsCoolingDown) {
                return;
            }
            
            if (charging) {
                var pos = 1f - GetNormalizedChargeProgress();
                coilAnimator.Play(CancelCharge, 0, pos);
                gunOverlayAnimator.Play(CancelCharge, 0, pos);
                
                fireSoundHandle?.Stop();
                fireSoundHandle = null;
            }

            audioPlayer.Play(windDown);

            base.EndCharge();
        }

        protected override void BlockUpdate() {
            var prevChargeTime = chargeTime;
            
            base.BlockUpdate();

            if (prevChargeTime != chargeTime) {
                var progress = GetNormalizedChargeProgress();
                
                HandleSound(progress);

                if (cursorFacade.Value.Enabled) {
                    cursorFacade.Value.Update(progress, IsCoolingDown);
                }

                if (electricityLoopHandle != null) {
                    electricityLoopHandle.audioSource.volume = progressToElectricityVolume.Evaluate(progress);
                }

                // Reset coil step and arcs when reaching 0 charge progress
                if (progress == 0f) {
                    prevCoilStep = -1;
                    arcs.Reset();
                    
                    // Remove if using a virtual cursor
                    S.UI.MouseCursor.CurrentCursor.CursorFacades?.Remove(cursorFacade);
                    
                    if (!charging) {
                        electricityLoopHandle?.Stop();
                        electricityLoopHandle = null;
                    }
                }
                
                arcs.SetChargeProgress(progress);
            } 
        }

        public override void Fire() {
            base.Fire();
            
            prevCoilStep = -1;
            arcs.Reset();

            audioPlayer.Play(fire);
            
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

        protected override void OnRemove() {
            base.OnRemove();

            // Remove if still present
            if (cursorFacade.Value.Enabled) {
                S.UI.MouseCursor.CurrentCursor.CursorFacades?.Remove(cursorFacade);
            }
        }

        private void HandleSound(float progress) {
            var coilStep = progress.DivRem(1f / 3f);
            
            if (charging && prevCoilStep < coilStep) {
                var sound = coilStep switch {
                    0 => coilCharge1,
                    1 => coilCharge2,
                    2 => coilCharge3,
                    _ => throw new ArgumentException("Invalid coilStep", nameof(coilStep))
                };

                audioPlayer.Play(sound);
            }

            prevCoilStep = coilStep;
        }
    }
}