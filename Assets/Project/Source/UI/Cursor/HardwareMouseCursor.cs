﻿using UnityEngine;

namespace Exa.UI
{
    public class HardwareMouseCursor : MonoBehaviour, ICursor
    {
        [SerializeField] private Texture2D idleMouseTexture;
        [SerializeField] private Texture2D activeMouseTexture;
        [SerializeField] private Texture2D removeMouseTexture;
        [SerializeField] private Texture2D infoMouseTexture;
        [SerializeField] private Texture2D inputMouseTexture;

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

        private Texture2D GetTexture(CursorState cursorState)
        {
            switch (cursorState)
            {
                case CursorState.idle:
                    return idleMouseTexture;

                case CursorState.active:
                    return activeMouseTexture;

                case CursorState.remove:
                    return removeMouseTexture;

                case CursorState.info:
                    return infoMouseTexture;

                case CursorState.input:
                    return inputMouseTexture;

                default:
                    return null;
            }
        }
    }
}