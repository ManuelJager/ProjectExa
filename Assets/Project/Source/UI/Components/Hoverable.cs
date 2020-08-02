using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Exa.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent onPointerEnter = new UnityEvent();
        public UnityEvent onPointerExit = new UnityEvent();

        public bool invokeStateChangeOnHover;
        public CursorState cursorState;

        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private bool mouseOverControl = false;

        private bool InvokeStateChange
        {
            get => invokeStateChangeOnHover && canvasGroup.interactable;
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (mouseOverControl) return;

            mouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
            {
                Systems.InputManager.OnHoverOverControl(this);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!mouseOverControl) return;

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                Systems.InputManager.OnExitControl();
            }
        }

        public void OnEnable()
        {
            this.DelayOneFrame(() =>
            {
                if (mouseOverControl) return;

                var mousePos = Systems.InputManager.ScaledMousePosition;
                if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
                {
                    mouseOverControl = true;
                    onPointerEnter?.Invoke();

                    if (InvokeStateChange)
                    {
                        Systems.InputManager.OnHoverOverControl(this);
                    }
                }
            });
        }

        public void OnDisable()
        {
            if (mouseOverControl)
            {
                mouseOverControl = false;
                onPointerExit?.Invoke();

                if (InvokeStateChange)
                {
                    MiscUtils.InvokeIfNotQuitting(() => Systems.InputManager.OnExitControl());
                }
            }
        }
    }
}