using Exa.Grids.Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        public void OnLeftClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (ShipSelection != null && ShipSelection.CanControl)
                    {
                        var point = Systems.InputManager.MouseWorldPoint;
                        ShipSelection.MoveTo(point);
                        return;
                    }

                    if (raycastTarget == null) return;

                    if (raycastTarget is FriendlyShip)
                    {
                        ShipSelection = new FriendlyShipSelection(raycastTarget as FriendlyShip);
                        return;
                    }

                    break;
            }
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    gameplayCameraController.SetMovementDelta(context.ReadValue<Vector2>());
                    break;

                case InputActionPhase.Canceled:
                    gameplayCameraController.SetMovementDelta(Vector2.zero);
                    break;

                default:
                    return;
            }
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (ShipSelection != null)
                    {
                        ShipSelection = null;
                    }
                    break;
            }
        }
    }
}