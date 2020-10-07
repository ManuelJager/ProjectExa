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

        [SerializeField] private bool _checkMouseInsideRectOnEnable = true;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private bool _mouseOverControl = false;

        private bool InvokeStateChange
        {
            get => invokeStateChangeOnHover && _canvasGroup.interactable;
        }

        private void Awake()
        {
            cursorOverride = new CursorOverride(cursorState);
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            if (_checkMouseInsideRectOnEnable)
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
            if (!_mouseOverControl)
            {
                throw new InvalidOperationException("May not force exit the control when not selected");
            }

            _mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                OnExit();
            }
        }

        private void TryEnter()
        {
            if (_mouseOverControl) return;

            _mouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
            {
                OnEnter();
            }
        }

        private void TryExit()
        {
            if (!_mouseOverControl) return;

            _mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                OnExit();
            }
        }

        private void CheckMouseInsideRect()
        {
            if (_mouseOverControl) return;

            if (Systems.Input.GetMouseInsideRect(_rectTransform))
            {
                TryEnter();
            }
        }

        private void OnEnter()
        {
            Systems.Ui.mouseCursor.AddOverride(cursorOverride);
        }

        private void OnExit()
        {
            Systems.Ui.mouseCursor.RemoveOverride(cursorOverride);
        }
    }
}