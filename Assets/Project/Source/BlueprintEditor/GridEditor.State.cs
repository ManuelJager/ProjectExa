using Exa.Validation;
using Exa.Utils;
using UnityEngine;

namespace Exa.ShipEditor
{
    public partial class GridEditor
    {
        private bool leftButtonPressed;
        private bool rightButtonPressed;
        private bool mouseOverUI;
        private bool interactible = true;
        private float zoom;
        private BlockFlip flipState;

        public bool IsSaved { get; private set; }
        public ValidationResult NameValidationResult { get; private set; }
        public ValidationResult GridValidationResult { get; private set; }
        public GridEditorImportArgs ImportArgs { get; set; }
        
        public BlockFlip FlipState {
            get => flipState;
            set {
                flipState = value;

                overlay.verticalMirrorView.SetState(value.HasValue(BlockFlip.FlipY));
                editorGrid.ghostLayer.SetFlip(value);
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
        }

        public void UpdateSaveButton() {
            ShouldSave = GetShouldSave(out var message);

            overlay.infoPanel.saveButtonTooltipTrigger.SetText(message);
            overlay.infoPanel.saveButtonCanvasGroup.interactable = ShouldSave;
            overlay.infoPanel.saveButtonCanvasGroup.alpha = ShouldSave ? 1f : 0.5f;
        }

    }
}