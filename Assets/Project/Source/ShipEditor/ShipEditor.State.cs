using Exa.Grids.Blueprints;
using Exa.Validation;
using System;
using Exa.UI;
using UnityEngine;

namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        private bool _leftButtonPressed = false;
        private bool _rightButtonPressed = false;
        private bool _mirrorEnabled = false;
        private bool _mouseOverUi = false;
        private bool _interactible = true;
        private float _zoom;
        private BlueprintContainer _container;
        private Action<BlueprintContainer> _saveCallback;

        public bool IsSaved { get; private set; }
        public ValidationResult NameValidationResult { get; private set; }
        public ValidationResult GridValidationResult { get; private set; }

        public bool MirrorEnabled
        {
            get => _mirrorEnabled;
            set
            {
                _mirrorEnabled = value;
                editorGrid.MirrorEnabled = value;
                _shipEditorOverlay.mirrorView.SetState(value);
            }
        }

        public bool Interactable
        {
            get => _interactible;
            set
            {
                _interactible = value;

                editorGrid.Interactable = value;
                navigateable.Interactable = value;

                if (value)
                {
                    _gameControls.Enable();
                }
                else
                {
                    _gameControls.Disable();
                    editorGrid.MovementVector = Vector2.zero;
                }
            }
        }

        public bool MouseOverUi
        {
            get => _mouseOverUi;
            set
            {
                _mouseOverUi = value;

                editorGrid.MouseOverUi = value;
            }
        }

        public float Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;

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
            NameValidationResult = null;
            GridValidationResult = null;

            Camera.main.orthographicSize = Zoom;
        }

        public void UpdateSaveButtonActive()
        {
            var valid = ShouldSave;

            _shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.interactable = valid;
            _shipEditorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.alpha = valid ? 1f : 0.5f;
        }
    }
}