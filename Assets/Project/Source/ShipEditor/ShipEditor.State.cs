using Exa.Grids.Blueprints;
using Exa.Validation;
using System;
using UnityEngine;

namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        private bool leftButtonPressed = false;
        private bool rightButtonPressed = false;
        private bool mirrorEnabled = false;
        private bool mouseOverUI = false;
        private bool interactible = true;
        private float zoom;
        private BlueprintContainer container;
        private Action<BlueprintContainer> saveCallback;

        public bool IsSaved { get; private set; }
        public ValidationResult NameValidationResult { get; private set; }
        public ValidationResult GridValidationResult { get; private set; }

        public bool MirrorEnabled {
            get => mirrorEnabled;
            set {
                mirrorEnabled = value;
                editorGrid.MirrorEnabled = value;
                overlay.mirrorView.SetState(value);
            }
        }

        public bool Interactable {
            get => interactible;
            set {
                interactible = value;

                editorGrid.Interactable = value;
                navigateable.Interactable = value;

                if (value) {
                    gameControls.Enable();
                }
                else {
                    gameControls.Disable();
                    editorGrid.MovementVector = Vector2.zero;
                }
            }
        }

        public bool MouseOverUI {
            get => mouseOverUI;
            set {
                mouseOverUI = value;

                editorGrid.MouseOverUI = value;
            }
        }

        public float Zoom {
            get => zoom;
            set {
                zoom = value;

                editorGrid.ZoomScale = value / 5f;
            }
        }

        private bool ShouldSave { get; set; }

        private void ResetState() {
            Zoom = 5f;
            IsSaved = true;
            NameValidationResult = null;
            GridValidationResult = null;

            Camera.main.orthographicSize = Zoom;
        }

        public void UpdateSaveButton() {
            ShouldSave = GetShouldSave(out var message);

            overlay.infoPanel.saveButtonTooltipTrigger.SetText(message);
            overlay.infoPanel.saveButtonCanvasGroup.interactable = ShouldSave;
            overlay.infoPanel.saveButtonCanvasGroup.alpha = ShouldSave ? 1f : 0.5f;
        }

        private bool GetShouldSave(out string message) {
            if (IsSaved) {
                message = "Blueprint is already saved";
                return false;
            }

            if (NameValidationResult != null && !NameValidationResult) {
                message = NameValidationResult.GetFirstBySeverity().Message;
                return false;
            }

            if (GridValidationResult != null && !GridValidationResult) {
                message = GridValidationResult.GetFirstBySeverity().Message;
                return false;
            }

            message = null;
            return true;
        }
    }
}