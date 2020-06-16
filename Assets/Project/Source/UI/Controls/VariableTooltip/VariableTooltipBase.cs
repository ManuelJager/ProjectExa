using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.UI.Controls
{
    public abstract class VariableTooltipBase<T> : MonoBehaviour
        where T : ITooltipPresenter
    {
        [SerializeField] protected RectTransform container;
        [SerializeField] protected Transform border;

        private void Update()
        {
            SmartSetPos();
        }

        public void ShowTooltip(T data)
        {
            gameObject.SetActive(true);

            foreach (Transform child in container)
            {
                if (border != child)
                {
                    Destroy(child.gameObject);
                }
            }

            SmartSetPos();
            SetValues(data);
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
            var mousePos = Mouse.current.position.ReadValue();
            // Get the edge of the container by adding the mouse position to the container size
            var edge = mousePos + container.sizeDelta;
            // Use 10 pixels as a sort of buffer
            edge.x -= 10f;
            edge.y -= 10f;

            // Calculate position offset
            var offset = new Vector2
            {
                x = Mathf.Clamp(Screen.width - edge.x, float.MinValue, 0),
                y = Mathf.Clamp(Screen.height - edge.y, float.MinValue, 0),
            };

            // Set position of container
            container.anchoredPosition = mousePos + offset;
        }

        public virtual void SetValues(T data)
        {
            VariableTooltipManager.Instance.tooltipGenerator.GenerateTooltip(data, container);
        }
    }
}