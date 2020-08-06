using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class VirtualMouseCursor : MonoBehaviour, ICursor
    {
        [SerializeField] private Image cursorBackground;
        [SerializeField] private Color idleColor;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color removeColor;
        [SerializeField] private Color infoColor;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform normalCursor;
        [SerializeField] private RectTransform inputCursor;
        private CursorState cursorState = CursorState.idle;

        private void OnEnable()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            rectTransform.anchoredPosition = Systems.InputManager.ScaledMousePosition;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState state)
        {
            SwitchActive(cursorState, false);
            SwitchActive(state, true);

            switch (state)
            {
                case CursorState.idle:
                    cursorBackground.color = idleColor;
                    break;

                case CursorState.active:
                    cursorBackground.color = activeColor;
                    break;

                case CursorState.remove:
                    cursorBackground.color = removeColor;
                    break;

                case CursorState.info:
                    cursorBackground.color = infoColor;
                    break;
            }
        }

        private void SwitchActive(CursorState state, bool active)
        {
            switch (state)
            {
                case CursorState.idle:
                case CursorState.active:
                case CursorState.remove:
                case CursorState.info:
                    normalCursor.gameObject.SetActive(active);
                    break;

                case CursorState.input:
                    inputCursor.gameObject.SetActive(active);
                    break;
            }
        }
    }
}