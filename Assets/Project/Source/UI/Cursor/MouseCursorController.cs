using Exa.Generics;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI
{
    public enum CursorType
    {
        HardwareCursor,
        VirtualCursor
    }

    public class MouseCursorController : MonoBehaviour
    {
        public CursorStateOverrideList stateManager;

        [SerializeField] private CursorType cursorType = CursorType.HardwareCursor;
        [SerializeField] private VirtualMouseCursor virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor hardwareMouseCursor;
        [SerializeField] private CursorState cursorState;
        private ICursor cursor;

        public bool MouseInViewport { get; private set; }
        public ICursor CurrentCursor => cursor;

        private void Start()
        {
            SetCursor(cursorType);
            cursor.SetState(cursorState);
            stateManager = new CursorStateOverrideList(cursorState, cursor.SetState);
            virtualMouseCursor.Init();
        }

        public void SetCursor(CursorType cursorType)
        {
            cursor?.SetActive(false);
            cursor = GetCursor(cursorType);
            cursor.SetActive(true);
        }

        public void UpdateMouseInViewport(bool value)
        {
            if (value != MouseInViewport)
            {
                MouseInViewport = value;
                SetMouseInViewport(value);
            }
        }

        public void SetMouseInViewport(bool value)
        {
            if (value)
            {
                cursor.OnEnterViewport();
            }
            else
            {
                cursor.OnExitViewport();
            }
        }

        private ICursor GetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.HardwareCursor:
                    return hardwareMouseCursor;

                case CursorType.VirtualCursor:
                    return virtualMouseCursor;

                default:
                    return null;
            }
        }
    }
}