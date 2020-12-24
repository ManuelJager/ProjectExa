using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class TextTooltip : FloatingTooltip
    {
        [SerializeField] private Text text;
        [SerializeField] private RectTransform verticalContainer;

        public static void ShowTooltip(string message) {
            Systems.UI.tooltips.textTooltip.ShowTooltipInternal(message);
        }

        public static void HideTooltip() {
            Systems.UI.tooltips.textTooltip.HideTooltipInternal();
        }

        protected override Vector2 GetTooltipSize() {
            return new Vector2 {
                x = verticalContainer.rect.x,
                y = base.GetTooltipSize().y
            };
        }

        private void ShowTooltipInternal(string message) {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            text.text = message;
            UpdatePosition(true);
        }

        private void HideTooltipInternal() {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}