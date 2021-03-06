﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        public void OnMovement(InputAction.CallbackContext context) {
            if (Systems.Input.inputIsCaptured) {
                editorGrid.MovementVector = Vector2.zero;
                return;
            }

            switch (context.phase) {
                case InputActionPhase.Performed:
                    editorGrid.MovementVector = context.ReadValue<Vector2>();
                    break;

                case InputActionPhase.Canceled:
                    editorGrid.MovementVector = Vector2.zero;
                    break;

                default:
                    return;
            }
        }

        public void OnLeftClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    leftButtonPressed = true;
                    break;

                case InputActionPhase.Canceled:
                    leftButtonPressed = false;
                    break;

                default:
                    return;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    rightButtonPressed = true;
                    break;

                case InputActionPhase.Canceled:
                    rightButtonPressed = false;
                    break;

                default:
                    return;
            }
        }

        public void OnRotateLeft(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    editorGrid.OnRotateLeft();
                    break;

                default:
                    return;
            }
        }

        public void OnRotateRight(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    editorGrid.OnRotateRight();
                    break;

                default:
                    return;
            }
        }

        public void OnToggleMirror(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    MirrorEnabled = !MirrorEnabled;
                    break;

                default:
                    return;
            }
        }

        public void OnZoom(InputAction.CallbackContext context) {
            if (MouseOverUI) return;

            if (context.phase == InputActionPhase.Performed) {
                var v2delta = context.ReadValue<Vector2>();
                var yDelta = v2delta.y;
                if (yDelta == 0f) return;

                yDelta /= 100f;
                Zoom = Mathf.Clamp(Zoom + (-yDelta * zoomSpeed), 3, 15);
                Camera.main.DOOrthoSize(Zoom, 0.5f);
            }
        }
    }
}