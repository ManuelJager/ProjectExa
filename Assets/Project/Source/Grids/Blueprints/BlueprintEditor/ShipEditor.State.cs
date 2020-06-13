using Assets.Project.Source.Utils;
using Exa.Utils;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public partial class ShipEditor
    {
        private bool leftButtonPressed = false;
        private bool rightButtonPressed = false;
        private bool lockMovement = false;
        private bool mirrorEnabled = false;
        private ObservableBlueprint blueprintContainer;
        private Blueprint newBlueprint;

        public bool MirrorEnabled
        {
            get => mirrorEnabled;
            set
            {
                mirrorEnabled = value;
                editorGrid.MirrorEnabled = value;
                editorOverlay.mirrorView.SetState(value);
            }
        }

        private bool interactible = true;

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

        private bool isSaved;

        public bool IsSaved
        {
            get => isSaved;
            set
            {
                isSaved = value;

                UpdateSaveButtonActive();
            }
        }

        private bool nameIsValid;

        public bool NameIsValid
        {
            get => nameIsValid;
            set
            {
                nameIsValid = value;

                UpdateSaveButtonActive();
            }
        }

        public bool Active
        {
            set
            {
                MiscUtils.InvokeIfNotQuitting(() => editorOverlay.gameObject.TrySetActive(value));
            }
        }

        public void UpdateSaveButtonActive()
        {
            var valid = !IsSaved && NameIsValid;

            editorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.interactable = valid;
            editorOverlay.blueprintInfoPanel.saveButtonCanvasGroup.alpha = valid ? 1f : 0.5f;
        }
    }
}