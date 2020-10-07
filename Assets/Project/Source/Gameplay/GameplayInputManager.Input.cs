﻿using System.Linq;
using Exa.Math;
using Exa.Ships;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private SelectionBuilder _selectionBuilder;

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
                    _gameplayCameraController.SetMovementDelta(context.ReadValue<Vector2>());
                    break;

                case InputActionPhase.Canceled:
                    _gameplayCameraController.SetMovementDelta(Vector2.zero);
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
                    if (Systems.GodModeIsEnabled && _raycastTarget is Ship ship)
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
            _selectionBuilder = new SelectionBuilder(worldPoint);
            GameSystems.Ui.selectionArea.Show(worldPoint);
        }

        private void OnUpdateSelectionArea()
        {
            var worldPoint = Systems.Input.MouseWorldPoint;
            GameSystems.Ui.selectionArea.SetEnd(worldPoint);
        }

        private void OnEndSelectionArea()
        {
            if (IsSelectingArea)
            {
                // Clear the current selection
                CurrentSelection?.Clear();

                // Update selection builder
                var worldPoint = Systems.Input.MouseWorldPoint;
                _selectionBuilder.UpdateSelection(worldPoint);

                // Hide the selection area overlay
                GameSystems.Ui.selectionArea.Hide();

                // Get the selection and save it in the hotbar if possible
                var selection = _selectionBuilder.Build();
                GameSystems.Ui.selectionHotbar.Save(selection);

                CurrentSelection = selection;

                _selectionBuilder = null;
            }
        }

        private void RemoveSelectionArea()
        {
            if (HasSelection && !IsSelectingArea)
            {
                // Deselect current selection
                var currentHotbarSelection = GameSystems.Ui.selectionHotbar.CurrentSelection;
                if (currentHotbarSelection != null)
                {
                    // Update the view
                    currentHotbarSelection.Selected = false;
                }

                // Clear the selection
                CurrentSelection.Clear();
                CurrentSelection = null;
            }
        }

        private void OnNumKey(int index)
        {
            if (HasSelection && GameSystems.Ui.selectionHotbar.CurrentSelection == null)
            {
                GameSystems.Ui.selectionHotbar.Select(index);
                GameSystems.Ui.selectionHotbar.Save(CurrentSelection);
            }
            else
            {
                CurrentSelection?.Clear();
                CurrentSelection = GameSystems.Ui.selectionHotbar.Select(index);
            }
        }
    }
}