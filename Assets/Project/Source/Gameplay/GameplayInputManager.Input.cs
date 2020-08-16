using Exa.Ships;
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
                    if (HasSelection && ShipSelection is FriendlyShipSelection)
                    {
                        var selection = ShipSelection as FriendlyShipSelection; 
                        var point = Systems.Input.MouseWorldPoint;
                        selection.MoveLookAt(point);
                        return;
                    }

                    if (Systems.GodModeIsAnabled && raycastTarget != null && raycastTarget is Ship)
                    {
                        var ship = raycastTarget as Ship;
                        ship.blockGrid.Totals.Hull -= 50f;
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

        public void OnNumKeys(InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    var rawValue = context.ReadValue<float>();
                    var index = Mathf.RoundToInt(rawValue);
                    OnNumKey(index);
                    break;
            }
        }

        private void OnStartSelectionArea()
        {
            var worldPoint = Systems.Input.MouseWorldPoint;
            selectionBuilder = new SelectionBuilder(worldPoint);
            GameSystems.UI.selectionArea.Show(worldPoint);
        }

        private void OnUpdateSelectionArea()
        {
            var worldPoint = Systems.Input.MouseWorldPoint;
            GameSystems.UI.selectionArea.SetEnd(worldPoint);
        }

        private void OnEndSelectionArea()
        {
            if (IsSelectingArea)
            {
                // Clear the current selection
                ShipSelection?.Clear();

                // Update selection builder
                var worldPoint = Systems.Input.MouseWorldPoint;
                selectionBuilder.UpdateSelection(worldPoint);

                // Hide the selection area overlay
                GameSystems.UI.selectionArea.Hide();

                // Get the selection and save it in the hotbar if possible
                var selection = selectionBuilder.Build();
                GameSystems.UI.selectionHotbar.Save(selection);

                ShipSelection = selection;

                selectionBuilder = null;
            }
        }

        private void RemoveSelectionArea()
        {
            if (HasSelection && !IsSelectingArea)
            {
                // Deselect current selection
                var currentHotbarSelection = GameSystems.UI.selectionHotbar.CurrentSelection;
                if (currentHotbarSelection != null)
                {
                    // Update the view
                    currentHotbarSelection.Selected = false;
                }

                // Clear the selection
                ShipSelection.Clear();
                ShipSelection = null;
            }
        }

        private void OnNumKey(int index)
        {
            if (HasSelection && GameSystems.UI.selectionHotbar.CurrentSelection == null)
            {
                GameSystems.UI.selectionHotbar.Select(index);
                GameSystems.UI.selectionHotbar.Save(ShipSelection);
            }
            else
            {
                ShipSelection?.Clear();
                ShipSelection = GameSystems.UI.selectionHotbar.Select(index);
            }
        }
    }
}