﻿using Exa.Types;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.UI.Cursor {
    public class HardwareMouseCursor : MonoBehaviour, ICursor {
        [SerializeField] private Texture2D idleMouseTexture;
        [SerializeField] private Texture2D activeMouseTexture;
        [SerializeField] private Texture2D removeMouseTexture;
        [SerializeField] private Texture2D infoMouseTexture;
        [SerializeField] private Texture2D inputMouseTexture;

        public MarkerContainer HoverMarkerContainer { get; } = null;

        public CursorFacades CursorFacades => null;
        
        private void OnEnable() {
            UnityEngine.Cursor.visible = true;
        }

        public void SetActive(bool active) {
            gameObject.SetActive(active);
        }

        public void SetState(CursorState cursorState) {
            var tex = GetTexture(cursorState);
            UnityEngine.Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
        }

        public void OnEnterViewport() { }

        public void OnExitViewport() { }

        private Texture2D GetTexture(CursorState cursorState) {
            switch (cursorState) {
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