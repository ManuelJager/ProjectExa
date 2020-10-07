using Exa.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Ships
{
    public class ShipOverlayHullBar : MonoBehaviour
    {
        public Image actualFillImage;
        public Image rememberedFillImage;

        [SerializeField] private readonly float _timeToUpdateRememberedFill = 3f;
        [SerializeField] private readonly float _rememberedFillSpeed = 0.4f;
        private float _actualFill = 1f;
        private float _rememberedFill = 1f;
        private float _timeSinceFillChange;

        private float ActualFill
        {
            get => _actualFill;
            set
            {
                _actualFill = value;
                actualFillImage.fillAmount = value;
            }
        }

        private float RememberedFill
        {
            get => _rememberedFill;
            set
            {
                _rememberedFill = value;
                rememberedFillImage.fillAmount = value;
            }
        }

        private void Update()
        {
            _timeSinceFillChange += Time.deltaTime;

            if (_timeSinceFillChange > _timeToUpdateRememberedFill)
            {
                var speed = Time.deltaTime * _rememberedFillSpeed;
                RememberedFill = MathUtils.Increment(RememberedFill, ActualFill, speed);
            }
        }

        public void SetFill(float value)
        {
            if (ActualFill == value) return;

            ActualFill = value;
            _timeSinceFillChange = 0f;
        }
    }
}