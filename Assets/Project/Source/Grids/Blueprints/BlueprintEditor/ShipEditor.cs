using Exa.Grids.Blocks;
using Exa.Input;
using Exa.UI;
using System;
using UnityEngine;
using static Exa.Input.GameControls;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public delegate void BlockSelectedDelegate(BlockTemplate id);

    [RequireComponent(typeof(ShipEditorNavigateable))]
    public partial class ShipEditor : MonoBehaviour, IEditorActions, IInteractableGroup
    {
        [HideInInspector] public ObservableBlueprintCollection blueprintCollection;
        public ShipEditorOverlay editorOverlay;
        public EditorGrid editorGrid;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        [SerializeField] private GameObject editorGridBackground;
        [SerializeField] private float zoomSpeed;
        private float zoom;
        private GameControls gameControls;
        private ShipEditorNavigateable navigateable;

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;

                editorGrid.ZoomScale = value / 5f;
            }
        }

        private void Awake()
        {
            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);
            navigateable = GetComponent<ShipEditorNavigateable>();
            SetGridBackground();

            editorOverlay.onPointerEnter.AddListener(() =>
            {
                BlockedByUI = true;
            });

            editorOverlay.onPointerExit.AddListener(() =>
            {
                BlockedByUI = false;
            });

            stopwatch.onTime.AddListener(ValidateGrid);

            Zoom = 5f;
        }

        private void Update()
        {
            if (leftButtonPressed)
            {
                editorGrid.OnLeftClickPressed();
                return;
            }

            if (rightButtonPressed)
            {
                editorGrid.OnRightClickPressed();
                return;
            }
        }

        private void OnEnable()
        {
            Active = true;
            gameControls.Enable();
            editorOverlay.inventory.BlockSelected += editorGrid.OnBlockSelected;
            editorOverlay.blueprintInfoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
        }

        private void OnDisable()
        {
            Active = false;
            gameControls.Disable();
            editorOverlay.inventory.BlockSelected -= editorGrid.OnBlockSelected;
            editorOverlay.blueprintInfoPanel.clearButton.onClick.RemoveListener(OnBlueprintClear);
        }

        public void Import(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            this.blueprintContainer = blueprintContainer;
            this.newBlueprint = blueprintContainer.Data.Clone();

            Zoom = 5f;
            editorGrid.Import(newBlueprint);
            editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.text = newBlueprint.name;

            SetCallbacks(blueprintContainer, saveCallback);
            ValidateName(blueprintContainer, newBlueprint.name);

            IsSaved = true;
        }

        public void SetCallbacks(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged.RemoveAllListeners();
            editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged.AddListener((value) =>
            {
                IsSaved = false;
                ValidateName(blueprintContainer, value);
            });

            editorOverlay.blueprintInfoPanel.saveButton.onClick.RemoveAllListeners();
            editorOverlay.blueprintInfoPanel.saveButton.onClick.AddListener(() =>
            {
                ValidateAndSave(blueprintContainer, saveCallback);
            });

            // Set saved state to false when blueprint data changes
            editorGrid.blueprintLayer.onBlueprintChanged.RemoveAllListeners();
            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(() =>
            {
                IsSaved = false;
            });
        }

        public void ValidateGrid()
        {
            var args = new BlueprintGridValidationArgs
            {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.blocks
            };

            editorOverlay.blueprintInfoPanel.errorListController.Validate(new BlueprintGridValidator(), args);
        }

        public void ValidateName(ObservableBlueprint blueprintContainer, string name)
        {
            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = name,
                blueprintContainer = blueprintContainer
            };

            var result = editorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintNameValidator(), args);

            NameIsValid = result.Valid;
        }

        public void ValidateAndSave(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            // Don't to save twice
            if (IsSaved) return;

            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.text,
                blueprintContainer = blueprintContainer
            };

            var result = editorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintNameValidator(), args);
            if (result.Valid)
            {
                IsSaved = true;
                newBlueprint.name = args.requestedName;
                blueprintContainer.Data = newBlueprint;
                saveCallback(blueprintContainer);
            }
            else
            {
                GameManager.Instance.promptController.PromptOk(result[0].Message, this);
            }
        }

        private void SetGridBackground()
        {
            var screenHeightInUnits = Camera.main.orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
            editorGridBackground.transform.localScale = new Vector3(screenWidthInUnits, screenHeightInUnits);
        }
    }
}