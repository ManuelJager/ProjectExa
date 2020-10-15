using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI.Tooltips
{
    public class TextTooltip : TooltipView
    {
        [SerializeField] private Text text;

        public static void ShowTooltip(string message)
        {
            Systems.UI.tooltips.textTooltip.ShowTooltipInternal(message);
        }

        public static void HideTooltip()
        {
            Systems.UI.tooltips.textTooltip.HideTooltipInternal();
        }

        protected override void SetAnchoredPos(Vector2 pos)
        {
            ((RectTransform)transform).anchoredPosition = pos;
        }

        private void ShowTooltipInternal(string message)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            text.text = message;
            SetContainerPosition();
        }

        private void HideTooltipInternal()
        {
            if (gameObject.activeSelf)
                gameObject.SetActive(false);
        }
    }
}