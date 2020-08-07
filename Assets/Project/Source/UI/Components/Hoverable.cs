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

        public CursorOverride cursorOverride;
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
            cursorOverride = new CursorOverride(cursorState);
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
                OnEnter();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!mouseOverControl) return;

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                OnExit();
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
                        OnEnter();
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
                    MiscUtils.InvokeIfNotQuitting(OnExit);
                }
            }
        }

        private void OnEnter()
        {
            Systems.MainUI.mouseCursor.AddOverride(cursorOverride);
        }

        private void OnExit()
        {
            Systems.MainUI.mouseCursor.RemoveOverride(cursorOverride);
        }
    }
}