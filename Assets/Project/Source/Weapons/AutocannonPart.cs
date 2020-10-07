using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
using Exa.Utils;
using Exa.Grids.Blocks;

namespace Exa.Weapons
{
    public class AutocannonPart : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private FiringPoint _firingPoint;
        [SerializeField] private Animation _barrelAnimator;
        [SerializeField] private Light2D _light2D;

        [Header("Settings")]
        [SerializeField] private float _peakIntensity;
        [SerializeField] private SpriteRenderer _drumRenderer;
        [SerializeField] private Sprite _drumNormal;
        [SerializeField] private Sprite _drumCycling;

        private float _animTime;

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        public void Setup(float animTime, ShipContext damageMask)
        {
            this._animTime = animTime;
            _barrelAnimator["BarrelAnimation"].speed = 1f / animTime;
            _firingPoint.Setup(damageMask);
        }

        public void Fire(float damage)
        {
            _barrelAnimator.Stop();
            _barrelAnimator.Play();

            _firingPoint.Fire(damage);

            _light2D.intensity = _peakIntensity;
            DOTween.To(() => _light2D.intensity, (value) => _light2D.intensity = value, 0, 0.1f);

            this.Delay(SetCyclingDrumSprite, _animTime * 0.25f);
            this.Delay(SetNormalDrumSprite, _animTime * 0.5f);
        }

        private void SetCyclingDrumSprite()
        {
            _drumRenderer.sprite = _drumCycling;
        }

        private void SetNormalDrumSprite()
        {
            _drumRenderer.sprite = _drumNormal;
        }
    }
}