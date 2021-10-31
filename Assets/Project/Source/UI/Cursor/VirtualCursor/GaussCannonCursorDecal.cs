using System;
using DG.Tweening;
using Exa.Data;
using Exa.Grids.Blocks.Components;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Cursor {
    public class GaussCannonCursorDecal : MonoBehaviour {
        [SerializeField] private GaussCannonCursorDecalCoil coil1;
        [SerializeField] private GaussCannonCursorDecalCoil coil2;
        [SerializeField] private GaussCannonCursorDecalCoil coil3;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ActivePair<float> targetAlpha;
        [SerializeField] private ActivePair<float> alphaAnimTime;
        
        private Tween activeTween;

        public void SetActive(bool active) {
            canvasGroup.DOFade(targetAlpha.GetValue(active), alphaAnimTime.GetValue(active))
                .Replace(ref activeTween);
        }
        
        public void SetProgress(float progress, bool coolingDown) {
            if (!coolingDown) {
                
                // Animate coils sequentially
                var coilStep = progress.DivRem(GaussCannonBehaviour.StepSize, out var rem);

                for (var i = 0; i < GaussCannonBehaviour.StepCount; i++) {
                    var coilProgress = coilStep > i
                        ? 1f // Progress for the current coil is 1 (Fully extended)
                        : coilStep == i // Whether or not to animate this coil
                            ? rem / GaussCannonBehaviour.StepSize // Divide remainder by step size to get a 0 - 1 normalized progress for the coil
                            : 0f; // Otherwise 0 progress (Fully retracted)
                    
                    CoilByIndex(i).SetProgress(coilProgress);
                }
            } else {
                // Cooldown animation animates all coils simultaneously
                for (var i = 0; i < GaussCannonBehaviour.StepCount; i++) {
                    CoilByIndex(i).SetProgress(progress);
                }
            }
        }

        private GaussCannonCursorDecalCoil CoilByIndex(int index) {
            return index switch {
                0 => coil1,
                1 => coil2,
                2 => coil3,
                _ => throw new ArgumentException("Index out of range", nameof(index))
            };
        }
    }
}