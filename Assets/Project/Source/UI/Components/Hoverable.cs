using Exa.Types.Generics;
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
        public ValueOverride<CursorState> cursorOverride;

        [SerializeField] private bool checkMouseInsideRectOnEnable = true;
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private bool mouseOverControl = false;

        private bool InvokeStateChange {
            get => invokeStateChangeOnHover && canvasGroup.interactable;
        }

        public bool MouseOverControl => mouseOverControl;

        private void Awake() {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable() {
            if (checkMouseInsideRectOnEnable)
                CheckMouseInsideRect();
        }

        private void OnDisable() {
            TryExit();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            TryEnter();
        }

        public void OnPointerExit(PointerEventData eventData) {
            TryExit();
        }

        public void ForceExit() {
            if (!mouseOverControl)
                return;

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
                OnExit();
        }

        public void Refresh() {
            CheckMouseInsideRect(true);
        }

        private void TryEnter() {
            if (mouseOverControl) return;

            mouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
                OnEnter();
        }

        private void TryExit() {
            if (!mouseOverControl) return;

            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
                OnExit();
        }

        private void CheckMouseInsideRect(bool exit = false) {
            if (mouseOverControl && !exit) return;

            if (Systems.Input.GetMouseInsideRect(rectTransform))
                TryEnter();
            else if (exit)
                TryExit();
        }

        private void OnEnter() {
            Systems.UI.mouseCursor.stateManager.Add(cursorOverride);
        }

        private void OnExit() {
            Systems.UI.mouseCursor.stateManager.Remove(cursorOverride);
        }
    }
}