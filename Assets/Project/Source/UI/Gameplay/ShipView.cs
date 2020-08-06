using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private Gradient colorGradient;
        [SerializeField] private Text countText;
        [SerializeField] private Slider slider;
        [SerializeField] private Image sliderImage;
        [SerializeField] private Image thumbnailImage;
        private int count;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                countText.text = count.ToString();
            }
        }

        public void SetThumbnail(Texture2D thumbnail)
        {
            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            thumbnailImage.sprite = Sprite.Create(thumbnail, thumbnailRect, thumbnailPivot);
        }

        public void SetHull(float value)
        {
            slider.value = value;
            sliderImage.color = colorGradient.Evaluate(value);
        }
    }
}