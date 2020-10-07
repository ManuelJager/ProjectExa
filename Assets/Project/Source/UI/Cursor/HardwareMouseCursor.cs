using UnityEngine;

namespace Exa.UI
{
    public class HardwareMouseCursor : MonoBehaviour, ICursor
    {
        [SerializeField] private Texture2D _idleMouseTexture;
        [SerializeField] private Texture2D _activeMouseTexture;
        [SerializeField] private Texture2D _removeMouseTexture;
        [SerializeField] private Texture2D _infoMouseTexture;
        [SerializeField] private Texture2D _inputMouseTexture;

        private void OnEnable()
        {
            Cursor.visible = true;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState cursorState)
        {
            var tex = GetTexture(cursorState);
            Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
        }

        public void OnEnterViewport()
        {
        }

        public void OnExitViewport()
        {
        }

        private Texture2D GetTexture(CursorState cursorState)
        {
            switch (cursorState)
            {
                case CursorState.Idle:
                    return _idleMouseTexture;

                case CursorState.Active:
                    return _activeMouseTexture;

                case CursorState.Remove:
                    return _removeMouseTexture;

                case CursorState.Info:
                    return _infoMouseTexture;

                case CursorState.Input:
                    return _inputMouseTexture;

                default:
                    return null;
            }
        }
    }
}