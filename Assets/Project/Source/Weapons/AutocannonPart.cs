using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using Exa.Utils;

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

        public void Setup(float firingSpeed)
        {
            animTime = firingSpeed * 1.5f;
            barrelAnimator["BarrelAnimation"].speed = 1f / animTime;
        }

        public void Fire(float damage)
        {
            barrelAnimator.Stop();
            barrelAnimator.Play();

            firingPoint.Fire(damage);

            light2D.intensity = peakIntensity;
            DOTween.To(() => light2D.intensity, (value) => light2D.intensity = value, 0, 0.1f);
            this.Delay(SetCyclingDrumSprite, animTime * 0.25f);
            this.Delay(SetNormalDrumSprite, animTime * 0.5f);
        }

        private void SetCyclingDrumSprite()
        {
            drumRenderer.sprite = drumCycling;
        }

        private void SetNormalDrumSprite()
        {
            drumRenderer.sprite = drumNormal;
        }
    }
}