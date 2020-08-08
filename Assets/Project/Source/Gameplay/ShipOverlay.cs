using Exa.Grids.Ships;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.Gameplay
{
    public class ShipOverlay : MonoBehaviour
    {
        public Ship ship;
        public RectTransform rectContainer;
        public Image actualFillImage;
        public Image rememberedFillImage;
        
        [SerializeField] private float timeToUpdateRememberedFill = 3f;
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
            transform.position = ship.transform.position;

            timeSinceFillChange += Time.deltaTime;

            if (timeSinceFillChange > timeToUpdateRememberedFill)
            {
                RememberedFill = MathUtils.Increment(RememberedFill, ActualFill, Time.deltaTime * 0.2f);
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