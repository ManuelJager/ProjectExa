using UnityEngine;

namespace Exa.UI.Tooltips
{
    public abstract class VariableTooltipBase<T> : MonoBehaviour
        where T : ITooltipPresenter
    {
        [SerializeField] protected RectTransform container;

        private TooltipBinder<T> binder;

        private void Update()
        {
            SetContainerPosition();
        }

        public void ShowTooltip(T data)
        {
            gameObject.SetActive(true);
            SetValues(data);
            SetContainerPosition();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Sets the position of the tooltip container while being aware of the screen edge
        /// </summary>
        public void SetContainerPosition()
        {
            // Get the mouse position
            var mousePos = Systems.InputManager.ScaledMousePosition;

            // Get the edge of the container by adding the mouse position to the container size
            var edge = mousePos + container.sizeDelta;

            // Add 4px (outline width) to edge size
            edge.x += 4f;
            edge.y += 4f;

            var rootRect = Systems.MainUI.rootTransform.rect;

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
            if (binder == null)
            {
                binder = Systems.MainUI.variableTooltipManager.tooltipGenerator.GenerateTooltip(data, container);
            }
            binder.Update(data);
        }
    }
}