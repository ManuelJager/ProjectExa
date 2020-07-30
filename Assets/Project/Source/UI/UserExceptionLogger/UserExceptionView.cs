using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class UserExceptionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Text text;

        public string Message
        {
            set => text.text = value;
        }

        private void Awake()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.DOFade(1, 0.2f);

            StartCoroutine(CoroutineUtils.Delay(3f, () =>
            {
                canvasGroup.DOFade(0f, 0.2f);
            }));

            StartCoroutine(CoroutineUtils.Delay(3.2f, () =>
            {
                Destroy(gameObject);
            }));
        }
    }
}