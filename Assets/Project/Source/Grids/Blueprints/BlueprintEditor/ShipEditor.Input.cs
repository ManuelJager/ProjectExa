using Exa.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public partial class ShipEditor
    {
        public void OnMovement(InputAction.CallbackContext context)
        {
            if (InputManager.Instance.InputIsCaptured)
            {
                editorGrid.MovementVector = Vector2.zero;
                return;
            }

            switch (context.phase)
            {
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

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
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

        public void OnRightClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
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

        public void OnRotateLeft(InputAction.CallbackContext context)
        {
            if (lockMovement)
            {
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    editorGrid.OnRotateLeft();
                    break;

                default:
                    return;
            }
        }

        public void OnRotateRight(InputAction.CallbackContext context)
        {
            if (lockMovement)
            {
                return;
            }

            switch (context.phase)
            {
                case InputActionPhase.Started:
                    editorGrid.OnRotateRight();
                    break;

                default:
                    return;
            }
        }

        public void OnToggleMirror(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    MirrorEnabled = !MirrorEnabled;
                    break;

                default:
                    return;
            }
        }

        public void OnBlueprintClear()
        {
            // Hide block ghost and ask user for blueprint clear confirmation
            GameManager.Instance.promptController.PromptYesNo("Are you sure you want to clear the blueprint?", this, (yes) =>
            {
                if (yes)
                {
                    editorGrid.ClearBlueprint();
                }
            });
        }
    }
}