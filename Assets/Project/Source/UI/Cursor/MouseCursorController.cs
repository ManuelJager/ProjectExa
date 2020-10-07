using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.UI
{
    public enum CursorType
    {
        HardwareCursor,
        VirtualCursor
    }

    public class MouseCursorController : MonoBehaviour
    {
        [SerializeField] private readonly CursorType _cursorType = CursorType.HardwareCursor;
        [SerializeField] private VirtualMouseCursor _virtualMouseCursor;
        [SerializeField] private HardwareMouseCursor _hardwareMouseCursor;
        private ICursor _cursor;
        private readonly List<CursorOverride> _cursorOverrides = new List<CursorOverride>();

        private void Start()
        {
            SetCursor(_cursorType);
            SetState(CursorState.Idle);
        }

        public void SetCursor(CursorType cursorType)
        {
            _cursor?.SetActive(false);
            _cursor = GetCursor(cursorType);
            _cursor.SetActive(true);
        }

        public void AddOverride(CursorOverride cursorOverride)
        {
            _cursorOverrides.Add(cursorOverride);

            SetState(cursorOverride.cursorState);
        }

        public void RemoveOverride(CursorOverride cursorOverride)
        {
            _cursorOverrides.Remove(cursorOverride);

            SetState(SelectStateFromOverrides());
        }

        private CursorState SelectStateFromOverrides()
        {
            return _cursorOverrides.Count == 0 
                ? CursorState.Idle 
                : _cursorOverrides.Last().cursorState;
        }

        private void SetState(CursorState cursorState)
        {
            _cursor.SetState(cursorState);
        }

        public void SetMouseInViewport(bool value)
        {
            if (value)
            {
                _cursor.OnEnterViewport();
            }
            else
            {
                _cursor.OnExitViewport();
            }
        }

        private ICursor GetCursor(CursorType cursorType)
        {
            switch (cursorType)
            {
                case CursorType.HardwareCursor:
                    return _hardwareMouseCursor;

                case CursorType.VirtualCursor:
                    return _virtualMouseCursor;

                default:
                    return null;
            }
        }
    }
}