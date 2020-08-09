using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class ShipOverlayHullBar : MonoBehaviour
    {
        public Image actualFillImage;
        public Image rememberedFillImage;

        [SerializeField] private float timeToUpdateRememberedFill = 3f;
        [SerializeField] private float rememberedFillSpeed = 0.4f;
        private float actualFill = 1f;
        private float rememberedFill = 1f;
        private float timeSinceFillChange;

        private float ActualFill
        {
            get => actualFill;
            set
            {
                actualFill = value;
                actualFillImage.fillAmount = value;
            }
        }

        private float RememberedFill
        {
            get => rememberedFill;
            set
            {
                rememberedFill = value;
                rememberedFillImage.fillAmount = value;
            }
        }

        private void Update()
        {
            timeSinceFillChange += Time.deltaTime;

            if (timeSinceFillChange > timeToUpdateRememberedFill)
            {
                var speed = Time.deltaTime * rememberedFillSpeed;
                RememberedFill = MathUtils.Increment(RememberedFill, ActualFill, speed);
            }
        }

        public void SetFill(float value)
        {
            if (ActualFill == value) return;

            ActualFill = value;
            timeSinceFillChange = 0f;
        }
    }
}