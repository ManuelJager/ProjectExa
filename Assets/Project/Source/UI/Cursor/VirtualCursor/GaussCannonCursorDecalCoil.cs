using Exa.UI.Tweening;
using Exa.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Cursor {
    public class GaussCannonCursorDecalCoil : MonoBehaviour {
        [SerializeField] private float magnitude;
        [SerializeField] private Image left;
        [SerializeField] private Image right;
        [SerializeField] private ExaEase progressToMagnitude;
        [SerializeField] private ExaEase progressToAlpha;

        public void SetProgress(float progress) {
            var magnitude = progressToMagnitude.Evaluate(progress) * this.magnitude;
            var alpha = progressToAlpha.Evaluate(progress);

            left.rectTransform.anchoredPosition = new Vector2(-magnitude, magnitude);
            right.rectTransform.anchoredPosition = new Vector2(magnitude, magnitude);

            left.color = left.color.SetAlpha(alpha);
            right.color = right.color.SetAlpha(alpha);
        }
    }
}