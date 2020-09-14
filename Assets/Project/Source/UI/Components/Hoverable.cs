using Exa.Utils;
using System;
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

        [SerializeField] private bool checkMouseInsideRectOnEnable = true;
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

        private void OnEnable()
        {
            if (checkMouseInsideRectOnEnable)
            {
                CheckMouseInsideRect();
            }
        }

        private void OnDisable()
        {
            TryExit();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            TryEnter();   
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TryExit();
        }

        public void ForceExit()
        {
            if (!mouseOverControl)
            {
                throw new InvalidOperationException("May not force exit the control when not selected");
            }

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                OnExit();
            }
        }

        private void TryEnter()
        {
            if (mouseOverControl) return;

            mouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
            {
                OnEnter();
            }
        }

        private void TryExit()
        {
            if (!mouseOverControl) return;

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                OnExit();
            }
        }

        private void CheckMouseInsideRect()
        {
            if (mouseOverControl) return;

            var mousePos = Systems.Input.ScreenPoint;
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos, Camera.main))
            {
                TryEnter();
            }
        }

        private void OnEnter()
        {
            Systems.UI.mouseCursor.AddOverride(cursorOverride);
        }

        private void OnExit()
        {
            Systems.UI.mouseCursor.RemoveOverride(cursorOverride);
        }
    }
}