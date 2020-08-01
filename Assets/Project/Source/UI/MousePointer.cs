using Exa.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public enum CursorState
    {
        idle,
        active,
        remove,
        info,
        input
    }

    public class MousePointer : MonoBehaviour
    {
        [HideInInspector] public CursorState cursorState = CursorState.idle;
        [HideInInspector] public CursorState prevCursorState = CursorState.idle;
        public Image cursorBackground;
        public Color idleColor;
        public Color activeColor;
        public Color removeColor;
        public Color infoColor;

        [SerializeField] private Transform normalCursor;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform inputCursor;

        private void OnEnable()
        {
            Cursor.visible = false;
        }

        private void OnDisable()
        {
            Cursor.visible = true;
        }

        public void SetState(CursorState state)
        {
            switch (cursorState)
            {
                case CursorState.idle:
                case CursorState.active:
                case CursorState.remove:
                case CursorState.info:
                    normalCursor.gameObject.SetActive(false);
                    break;

                case CursorState.input:
                    inputCursor.gameObject.SetActive(false);
                    break;
            }

            switch (state)
            {
                case CursorState.idle:
                    normalCursor.gameObject.SetActive(true);
                    cursorBackground.color = idleColor;
                    break;

                case CursorState.active:
                    normalCursor.gameObject.SetActive(true);
                    cursorBackground.color = activeColor;
                    break;

                case CursorState.remove:
                    normalCursor.gameObject.SetActive(true);
                    cursorBackground.color = removeColor;
                    break;

                case CursorState.info:
                    normalCursor.gameObject.SetActive(true);
                    cursorBackground.color = infoColor;
                    break;

                case CursorState.input:
                    inputCursor.gameObject.SetActive(true);
                    break;
            }

            prevCursorState = cursorState;
            cursorState = state;
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Systems.InputManager.ScaledMousePosition;
        }
    }
}