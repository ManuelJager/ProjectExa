using DG.Tweening;
using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Components
{
    public class AnimatedTabContent : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float delay;
        [SerializeField] public RectTransform target;

        public void HandleExit()
        {
            canvasGroup.interactable = false;
            canvasGroup.alpha = 1f;
            canvasGroup.DOFade(0, delay);
            target.anchoredPosition = Vector2.zero;
            target.DOAnchorPos(Vector2.left * 120, delay);
            this.Delay(() => gameObject.SetActive(false), delay);
        }

        public void HandleEnter()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1f, delay);
            target.anchoredPosition = Vector2.right * 120;
            target.DOAnchorPos(Vector2.zero, delay);
            this.Delay(() => canvasGroup.interactable = true, delay);
        }
    }
}