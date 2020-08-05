using Exa.Utils;
using Exa.Validation;
using System;
using System.Collections;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
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
        private Func<BlueprintContainer, IEnumerator> saveCallback;

        public bool IsSaved { get; private set; }
        public ValidationResult NameValidationResult { get; private set; }
        public ValidationResult GridValidationResult { get; private set; }

        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorEnabled = value;
                editorGrid.MirrorEnabled = value;
                shipEditorOverlay.mirrorView.SetState(value);
            }
        }

        public bool Interactable
        {
            get => interactible;
            set
            {
                interactible = value;

                editorGrid.Interactable = value;
                navigateable.Interactable = value;

                if (value)
                {
                    gameControls.Enable();
                }
                else
                {
                    gameControls.Disable();
                    editorGrid.MovementVector = Vector2.zero;
                }
            }
        }

        public bool MouseOverUI
        {
            get => mouseOverUI;
            set
            {
                mouseOverUI = value;

                editorGrid.MouseOverUI = value;
            }
        }

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;

                editorGrid.ZoomScale = value / 5f;
            }
        }

        private bool ShouldSave
        {
            get => !IsSaved && NameValidationResult && GridValidationResult;
        }

        private void ResetState()
        {
            Zoom = 5f;
            IsSaved = true;
            NameValidationResult = new ValidationResult();
            GridValidationResult = new ValidationResult();

            Camera.main.orthographicSize = Zoom;
        }

        public void UpdateSaveButtonActive()
        {
            var valid = ShouldSave;

            shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.interactable = valid;
            shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.alpha = valid ? 1f : 0.5f;
        }
    }
}