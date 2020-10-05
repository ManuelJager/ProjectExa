using DG.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Components
{
    public class AnimatedTabContent : MonoBehaviour
    {
        public RectTransform target;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float delay;

        public void HandleExit(Vector2 direction)
        {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0, delay);
            target.anchoredPosition = Vector2.zero;
            target.DOAnchorPos(direction * 120, delay);
            this.Delay(() => gameObject.SetActive(false), delay);
        }

        public void HandleEnter(Vector2 direction)
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, delay);
            target.anchoredPosition = direction * 120;
            target.DOAnchorPos(Vector2.zero, delay);
            this.Delay(() => canvasGroup.interactable = true, delay);
        }
    }
}