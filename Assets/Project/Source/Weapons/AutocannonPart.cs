﻿using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using Exa.Grids;
using Exa.Utils;
using Exa.Grids.Blocks;

#pragma warning disable CS0649

namespace Exa.Weapons
{
    public class AutocannonPart : MonoBehaviour
    {
        [Header("References")] 
        [SerializeField] private FiringPoint firingPoint;
        [SerializeField] private Animation barrelAnimator;
        [SerializeField] private Light2D light2D;

        [Header("Settings")] 
        [SerializeField] private float peakIntensity;
        [SerializeField] private SpriteRenderer drumRenderer;
        [SerializeField] private Sprite drumNormal;
        [SerializeField] private Sprite drumCycling;

        private float animTime;

        private void OnDisable() {
            StopAllCoroutines();
        }

        public void Setup(float animTime, IGridInstance parent, BlockContext damageMask) {
            this.animTime = animTime;
            barrelAnimator["BarrelAnimation"].speed = 1f / animTime;
            firingPoint.Setup(parent, damageMask);
        }

        public void Fire(float damage) {
            barrelAnimator.Stop();
            barrelAnimator.Play();

            firingPoint.Fire(damage);

            light2D.intensity = peakIntensity;
            light2D.DOIntensity(0, 0.1f);

            this.DelayLocally(SetCyclingDrumSprite, animTime * 0.25f);
            this.DelayLocally(SetNormalDrumSprite, animTime * 0.5f);
        }

        private void SetCyclingDrumSprite() {
            drumRenderer.sprite = drumCycling;
        }

        private void SetNormalDrumSprite() {
            drumRenderer.sprite = drumNormal;
        }
    }
}