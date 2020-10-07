using UnityEngine;

namespace Exa.Ships
{
    public class ShipOverlayCircle : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private bool _isSelected;
        private bool _isHovered;

        private void Awake()
        {
            CalculateState();
        }

        public bool IsSelected
        {
            private get => _isSelected;
            set
            {
                _isSelected = value;
                CalculateState();
            }
        }

        public bool IsHovered
        {
            private get => _isHovered;
            set
            {
                _isHovered = value;
                CalculateState();
            }
        }

        private void CalculateState()
        {
            gameObject.SetActive(IsSelected || IsHovered);

            if (IsSelected)
            {
                _canvasGroup.alpha = 0.5f;
            }
            else if (IsHovered)
            {
                _canvasGroup.alpha = 0.2f;
            }
        }
    }
}