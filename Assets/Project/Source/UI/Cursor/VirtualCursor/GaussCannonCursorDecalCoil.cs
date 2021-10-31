using Exa.UI.Tweening;
using Exa.Utils;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Cursor {
    public class GaussCannonCursorDecalCoil : MonoBehaviour {
        [SerializeField] private float magnitude;
        [SerializeField] private Image left;
        [SerializeField] private Image right;
        [SerializeField] private ExaEase progressToMagnitude;
        [SerializeField] private Gradient gradientMap;

        public void SetProgress(float progress) {
            var currentMagnitude = progressToMagnitude.Evaluate(progress) * magnitude;
            left.rectTransform.anchoredPosition = new Vector2(-currentMagnitude, currentMagnitude);
            right.rectTransform.anchoredPosition = new Vector2(currentMagnitude, currentMagnitude);

            var color = gradientMap.Evaluate(progress);
            left.color = color;
            right.color = color;
        }
    }
}