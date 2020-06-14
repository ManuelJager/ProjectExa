using Exa.Input;
using Exa.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Exa.UI
{
    [Serializable]
    public class HoverEvent : UnityEvent
    {
    }

    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public HoverEvent onPointerEnter;
        public HoverEvent onPointerExit;

        public bool invokeStateChangeOnHover;
        public CursorState cursorState;

        private RectTransform rectTransform;
        private bool queuedEnabled = false;
        private CanvasGroup canvasGroup;

        private bool InvokeStateChange
        {
            get => invokeStateChangeOnHover && canvasGroup.interactable;
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Update()
        {
            // Queue a check for mouse input so other behaviours have time to subscribe to the pointer enter event
            if (queuedEnabled)
            {
                var mousePos = Mouse.current.position.ReadValue();
                if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
                {
                    onPointerEnter?.Invoke();
                }
                queuedEnabled = false;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
            {
                InputManager.Instance.OnHoverOverControl(cursorState);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                InputManager.Instance.OnExitControl();
            }
        }

        public void OnEnable()
        {
            // Set queue flag
            queuedEnabled = true;
        }

        public void OnDisable()
        {
            if (InvokeStateChange)
            {
                MiscUtils.InvokeIfNotQuitting(InputManager.Instance.OnExitControl);
            }
        }
    }
}