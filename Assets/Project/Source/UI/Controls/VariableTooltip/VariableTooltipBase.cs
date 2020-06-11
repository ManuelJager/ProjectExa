using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.UI.Controls
{
    public abstract class VariableTooltipBase<T> : MonoBehaviour
    {
        [SerializeField] protected RectTransform container;

        private void Update()
        {
            var mousePos = Mouse.current.position.ReadValue();
            container.anchoredPosition = mousePos;
        }

        public void ShowTooltip(T data)
        {
            gameObject.SetActive(true);
            var mousePos = Mouse.current.position.ReadValue();
            container.anchoredPosition = mousePos;
            SetValues(data);
        }

        public abstract void SetValues(T data);

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }
    }
}