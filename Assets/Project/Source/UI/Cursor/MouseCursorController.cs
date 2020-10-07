using System.Collections.Generic;
using System.Linq;
using Exa.Generics;
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
        public OverrideList<CursorState> stateManager;

        [SerializeField] private CursorType cursorType = CursorType.hardwareCursor;
        [SerializeField] private VirtualMouseCursor virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor hardwareMouseCursor;
        [SerializeField] private CursorState cursorState;
        private ICursor cursor;

        public ICursor CurrentCursor => cursor;

        private void Start()
        {
            SetCursor(cursorType);
            cursor.SetState(cursorState);
            stateManager = new OverrideList<CursorState>(cursorState, cursor.SetState);
        }

        public void SetCursor(CursorType cursorType)
        {
            cursor?.SetActive(false);
            cursor = GetCursor(cursorType);
            cursor.SetActive(true);
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