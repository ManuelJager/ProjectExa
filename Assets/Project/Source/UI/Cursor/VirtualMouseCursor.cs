using UnityEngine;
using UnityEngine.UI;

namespace Exa.UI
{
    public class VirtualMouseCursor : MonoBehaviour, ICursor
    {
        [SerializeField] private Image _cursorBackground;
        [SerializeField] private Color _idleColor;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _removeColor;
        [SerializeField] private Color _infoColor;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private RectTransform _normalCursor;
        [SerializeField] private RectTransform _inputCursor;
        private readonly CursorState _cursorState = CursorState.Idle;

        private void OnEnable()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            _rectTransform.anchoredPosition = Systems.Input.ScaledViewportPoint;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState state)
        {
            SwitchActive(_cursorState, false);
            SwitchActive(state, true);

            switch (state)
            {
                case CursorState.Idle:
                    _cursorBackground.color = _idleColor;
                    break;

                case CursorState.Active:
                    _cursorBackground.color = _activeColor;
                    break;

                case CursorState.Remove:
                    _cursorBackground.color = _removeColor;
                    break;

                case CursorState.Info:
                    _cursorBackground.color = _infoColor;
                    break;
            }
        }

        public void OnEnterViewport()
        {
            gameObject.SetActive(true);
        }

        public void OnExitViewport()
        {
            gameObject.SetActive(false);
        }

        private void SwitchActive(CursorState state, bool active)
        {
            switch (state)
            {
                case CursorState.Idle:
                case CursorState.Active:
                case CursorState.Remove:
                case CursorState.Info:
                    _normalCursor.gameObject.SetActive(active);
                    break;

                case CursorState.Input:
                    _inputCursor.gameObject.SetActive(active);
                    break;
            }
        }
    }
}