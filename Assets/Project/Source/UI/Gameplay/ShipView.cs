using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
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
        private List<Ship> ships = new List<Ship>();

        public int Count
        {
            get => count;
            private set
            {
                count = value;
                countText.text = count.ToString();
            }
        }

        private void Update()
        {
            var total = ships.Average((ship) => ship.state.Hull);
            SetHull(total);
        }

        public void SetThumbnail(Texture2D thumbnail)
        {
            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            thumbnailImage.sprite = Sprite.Create(thumbnail, thumbnailRect, thumbnailPivot);
        }

        public void Add(Ship ship)
        {
            ships.Add(ship);
            Count++;
        }

        public void Remove(Ship ship)
        {
            ships.Remove(ship);
            Count--;
        }

        private void SetHull(float value)
        {
            slider.value = value;
            sliderImage.color = colorGradient.Evaluate(value);
        }
    }
}