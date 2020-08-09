using Exa.Grids.Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private SelectionBuilder selectionBuilder;

        public void OnLeftClick(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnStartSelectionArea();
                    break;

                case InputActionPhase.Canceled:
                    OnEndSelectionArea();
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
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    if (HasSelection && ShipSelection.CanControl)
                    {
                        var point = Systems.InputManager.MouseWorldPoint;
                        ShipSelection.MoveTo(point);
                        return;
                    }

                    if (Systems.GodModeIsAnabled && raycastTarget != null && raycastTarget is Ship)
                    {
                        var ship = raycastTarget as Ship;
                        ship.blockGrid.CurrentHull -= 50f;
                    }
                    break;
            }
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
        }

        public void OnEscape(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    RemoveSelectionArea();
                    break;
            }
        }

        private void OnStartSelectionArea() 
        {
            if (!HasSelection)
            {
                var worldPoint = Systems.InputManager.MouseWorldPoint;
                selectionBuilder = new SelectionBuilder(worldPoint);
                GameSystems.GameplayUI.selectionArea.Show(worldPoint);
            }
        }

        private void OnUpdateSelectionArea()
        {
            var worldPoint = Systems.InputManager.MouseWorldPoint;
            GameSystems.GameplayUI.selectionArea.SetEnd(worldPoint);
        }

        private void OnEndSelectionArea()
        {
            if (IsSelectingArea)
            {
                var worldPoint = Systems.InputManager.MouseWorldPoint;
                selectionBuilder.UpdateSelection(worldPoint);
                GameSystems.GameplayUI.selectionArea.Hide();
                ShipSelection = selectionBuilder.Build();
                selectionBuilder = null;
            }
        }

        private void RemoveSelectionArea()
        {
            if (HasSelection && !IsSelectingArea)
            {
                ShipSelection.Clear();
                ShipSelection = null;
            }
        }
    }
}