using DG.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class UserExceptionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text _text;

        public string Message
        {
            set => _text.text = value;
        }

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1, 0.2f);

            StartCoroutine(EnumeratorUtils.Delay(() =>
            {
                _canvasGroup.DOFade(0f, 0.2f);
            }, 3f));

            StartCoroutine(EnumeratorUtils.Delay(() =>
            {
                Destroy(gameObject);
            }, 3.2f));
        }
    }
}