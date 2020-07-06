using Exa.Input;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public abstract class VariableTooltipBase<T> : MonoBehaviour
        where T : ITooltipPresenter
    {
        [SerializeField] protected RectTransform container;

        private void Update()
        {
            SmartSetPos();
        }

        public void ShowTooltip(T data)
        {
            gameObject.SetActive(true);
            SetValues(data);
            SmartSetPos();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the position of the tooltip container while being aware of the screen edge
        /// </summary>
        public void SmartSetPos()
        {
            // Get the mouse position
            var mousePos = InputManager.Instance.ScaledMousePosition;

            // Get the edge of the container by adding the mouse position to the container size
            var edge = mousePos + container.sizeDelta;

            // Add 4px (outline width) to edge size
            edge.x += 4f;
            edge.y += 4f;

            var rootRect = UIManager.Instance.RootTransform.rect;

            // Calculate position offset
            var offset = new Vector2
            {
                x = Mathf.Clamp(rootRect.width - edge.x, float.MinValue, 0),
                y = Mathf.Clamp(rootRect.height - edge.y, float.MinValue, 0),
            };

            // Set position of container
            container.anchoredPosition = mousePos + offset;
        }

        public virtual void SetValues(T data)
        {
            var result = data.GetComponents();
            VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltip(result, container);
        }
    }
}