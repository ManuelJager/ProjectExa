using DG.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Components
{
    public class AnimatedTabContent : MonoBehaviour
    {
        public RectTransform target;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _delay;

        public void HandleExit(Vector2 direction)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 1f;
            _canvasGroup.DOFade(0, _delay);
            target.anchoredPosition = Vector2.zero;
            target.DOAnchorPos(direction * 120, _delay);
            this.Delay(() => gameObject.SetActive(false), _delay);
        }

        public void HandleEnter(Vector2 direction)
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, _delay);
            target.anchoredPosition = direction * 120;
            target.DOAnchorPos(Vector2.zero, _delay);
            this.Delay(() => _canvasGroup.interactable = true, _delay);
        }
    }
}