using Exa.Input;
using Exa.Utils;
using System.Collections;
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
            mouseOverControl = true;
            onPointerEnter?.Invoke();

            if (InvokeStateChange)
            {
                InputManager.Instance.OnHoverOverControl(cursorState);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            mouseOverControl = false;
            onPointerExit?.Invoke();

            if (InvokeStateChange)
            {
                InputManager.Instance.OnExitControl();
            }
        }

        public void OnEnable()
        {
            StartCoroutine(DelayMouseOver());
        }

        public void OnDisable()
        {
            if (mouseOverControl)
            {
                onPointerExit?.Invoke();
            }

            if (InvokeStateChange)
            {
                MiscUtils.InvokeIfNotQuitting(() => InputManager.Instance.OnExitControl());
            }
        }

        private IEnumerator DelayMouseOver()
        {
            yield return 0;

            var mousePos = InputManager.Instance.ScaledMousePosition;
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, mousePos))
            {
                mouseOverControl = true;
                onPointerEnter?.Invoke();
            }
        }
    }
}