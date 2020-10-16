using Exa.Math;
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
                    if (HasSelection && CurrentSelection is FriendlyShipSelection selection)
                    {
                        var point = Systems.Input.MouseWorldPoint;
                        selection.MoveLookAt(point);
                        return;
                    }

                    //if (Systems.GodModeIsEnabled && raycastTarget != null && raycastTarget is Ship)
                    //{
                    //    var ship = raycastTarget as Ship;
                    //    ship.BlockGrid.Totals.Hull -= 50f;
                    //}
                    if (Systems.GodModeIsEnabled && raycastTarget is Ship ship)
                    {
                        var worldPos = ship.transform.position.ToVector2();
                        var direction = (worldPos - Systems.Input.MouseWorldPoint).normalized * ship.Totals.Mass;
                        ship.rb.AddForce(direction, ForceMode2D.Force);
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
                    if (!HasSelection)
                    {
                        GameSystems.UI.TogglePause();
                        return;
                    }

                    if (HasSelection && !IsSelectingArea)
                    {
                        RemoveSelectionArea();
                        return;
                    }

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
            GameSystems.UI.gameplayLayer.selectionArea.Show(worldPoint);
        }

        private void OnUpdateSelectionArea()
        {
            var worldPoint = Systems.Input.MouseWorldPoint;
            GameSystems.UI.gameplayLayer.selectionArea.SetEnd(worldPoint);
        }

        private void OnEndSelectionArea()
        {
            if (IsSelectingArea)
            {
                // Clear the current selection
                CurrentSelection?.Clear();

                // Update selection builder
                var worldPoint = Systems.Input.MouseWorldPoint;
                selectionBuilder.UpdateSelection(worldPoint);

                // Hide the selection area overlay
                GameSystems.UI.gameplayLayer.selectionArea.Hide();

                // Get the selection and save it in the hotbar if possible
                var selection = selectionBuilder.Build();
                GameSystems.UI.gameplayLayer.selectionHotbar.Save(selection);

                CurrentSelection = selection;

                selectionBuilder = null;
            }
        }

        private void RemoveSelectionArea()
        {
            // Deselect current selection
            var currentHotbarSelection = GameSystems.UI.gameplayLayer.selectionHotbar.CurrentSelection;
            if (currentHotbarSelection != null)
            {
                // Update the view
                currentHotbarSelection.Selected = false;
            }

            // Clear the selection
            CurrentSelection.Clear();
            CurrentSelection = null;
        }

        private void OnNumKey(int index)
        {
            if (HasSelection && GameSystems.UI.gameplayLayer.selectionHotbar.CurrentSelection == null)
            {
                GameSystems.UI.gameplayLayer.selectionHotbar.Select(index);
                GameSystems.UI.gameplayLayer.selectionHotbar.Save(CurrentSelection);
            }
            else
            {
                CurrentSelection?.Clear();
                CurrentSelection = GameSystems.UI.gameplayLayer.selectionHotbar.Select(index);
            }
        }
    }
}