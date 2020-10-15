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
                if (message != "")
                    TextTooltip.ShowTooltip(message);
            });

            hoverable.onPointerExit.AddListener(TextTooltip.HideTooltip);
        }

        public void SetText(string message)
        {
            this.message = message;

            if (hoverable.MouseOverControl && message == "")
                TextTooltip.HideTooltip();

            if (hoverable.MouseOverControl && message != "")
                TextTooltip.ShowTooltip(message);
        }
    }
}