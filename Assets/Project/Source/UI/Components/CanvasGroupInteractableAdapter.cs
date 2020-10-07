using UnityEngine;

namespace Exa.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupInteractableAdapter : MonoBehaviour, IUiGroup
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _setAlpha;
        [SerializeField] private bool _interactible = true;

        public bool Interactable
        {
            get => _interactible;
            set
            {
                if (_interactible == value) return;

                _interactible = value;

                if (_canvasGroup != null)
                {
                    _canvasGroup.blocksRaycasts = value;
                    _canvasGroup.interactable = value;
                    if (_setAlpha) _canvasGroup.alpha = value ? 1f : 0.5f;
                }
            }
        }

        private void Awake()
        {
            Interactable = _interactible;
            _canvasGroup = GetComponent<CanvasGroup>();
        }
    }
}