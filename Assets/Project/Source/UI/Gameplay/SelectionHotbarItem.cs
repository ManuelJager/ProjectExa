using Exa.Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Gameplay
{
    public class SelectionHotbarItem : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _text;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _emptyColor;
        private ShipSelection _shipSelection;
        private bool _selected;

        private void Awake()
        {
            _selected = false;

            UpdateView();
        }

        public void Setup(int index)
        {
            _text.text = index.ToString();
        }

        public ShipSelection ShipSelection
        {
            get => _shipSelection;
            set
            {
                _shipSelection = value;
                UpdateView();
            }
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
                UpdateView();
            }
        }

        private void UpdateView()
        {
            _canvasGroup.alpha = _selected
                ? 1f
                : 0.6f;

            _text.color = _shipSelection != null
                ? _selected
                    ? _activeColor
                    : _inactiveColor
                : _emptyColor;
        }
    }
}