using UnityEngine;

namespace Exa.UI
{
    public enum CursorType
    {
        hardwareCursor,
        virtualCursor
    }

    public class MouseCursorController : MonoBehaviour
    {
        [SerializeField] private CursorType cursorType = CursorType.hardwareCursor;
        [SerializeField] private VirtualMouseCursor virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor hardwareMouseCursor;
        private ICursor cursor;

        private void Start()
        {
            SetCursor(cursorType);
            SetState(CursorState.idle);
        }

        public void SetCursor(CursorType cursorType)
        {
            cursor?.SetActive(false);
            cursor = GetCursor(cursorType);
            cursor.SetActive(true);
        }

        public void SetState(CursorState cursorState)
        {
            cursor.SetState(cursorState);
        }

        private ICursor GetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.hardwareCursor:
                    return hardwareMouseCursor;

                case CursorType.virtualCursor:
                    return virtualMouseCursor;

                default:
                    return null;
            }
        }
    }
}