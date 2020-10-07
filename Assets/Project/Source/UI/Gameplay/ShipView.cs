using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class ShipView : MonoBehaviour
    {
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private Text _countText;
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _sliderImage;
        [SerializeField] private Image _thumbnailImage;
        private int _count;
        private readonly List<Ship> _ships = new List<Ship>();

        public int Count
        {
            get => _count;
            private set
            {
                _count = value;
                _countText.text = _count.ToString();
            }
        }

        private void Update()
        {
            var total = _ships.Average((ship) => ship.state.HullIntegrity);
            SetHull(total);
        }

        public void SetThumbnail(Texture2D thumbnail)
        {
            var thumbnailRect = new Rect(0, 0, 512, 512);
            var thumbnailPivot = new Vector2(0.5f, 0.5f);
            _thumbnailImage.sprite = Sprite.Create(thumbnail, thumbnailRect, thumbnailPivot);
        }

        public void Add(Ship ship)
        {
            _ships.Add(ship);
            Count++;
        }

        public void Remove(Ship ship)
        {
            _ships.Remove(ship);
            Count--;
        }

        private void SetHull(float value)
        {
            _slider.value = value;
            _sliderImage.color = _colorGradient.Evaluate(value);
        }
    }
}