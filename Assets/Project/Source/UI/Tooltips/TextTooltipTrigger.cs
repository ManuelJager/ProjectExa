using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Tooltips
{
    [RequireComponent(typeof(Hoverable))]
    public class TextTooltipTrigger : MonoBehaviour
    {
        [SerializeField] private Hoverable hoverable;
        [SerializeField] private string message;

        private void Awake()
        {
            hoverable = hoverable ?? GetComponent<Hoverable>();
            hoverable.onPointerEnter.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(message))
                    TextTooltip.ShowTooltip(message);
            });

            hoverable.onPointerExit.AddListener(TextTooltip.HideTooltip);
        }

        public void SetText(string message)
        {
            this.message = message;
            var stringNullOrEmpty = string.IsNullOrEmpty(message);

            if (hoverable.MouseOverControl && stringNullOrEmpty)
                TextTooltip.HideTooltip();

            if (hoverable.MouseOverControl && !stringNullOrEmpty)
                TextTooltip.ShowTooltip(message);
        }
    }
}