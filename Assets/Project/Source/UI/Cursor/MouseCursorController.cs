using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private readonly CursorType cursorType = CursorType.hardwareCursor;
        [SerializeField] private VirtualMouseCursor virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor hardwareMouseCursor;
        private ICursor cursor;
        private readonly List<CursorOverride> cursorOverrides = new List<CursorOverride>();

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

        public void AddOverride(CursorOverride cursorOverride)
        {
            cursorOverrides.Add(cursorOverride);

            SetState(cursorOverride.cursorState);
        }

        public void RemoveOverride(CursorOverride cursorOverride)
        {
            cursorOverrides.Remove(cursorOverride);

            SetState(SelectStateFromOverrides());
        }

        private CursorState SelectStateFromOverrides()
        {
            if (cursorOverrides.Count == 0)
            {
                return CursorState.idle;
            }
            else
            {
                return cursorOverrides.Last().cursorState;
            }
        }

        private void SetState(CursorState cursorState)
        {
            cursor.SetState(cursorState);
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