using Exa.Grids.Blocks;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using Exa.Utils;
using System;
using UnityEngine;
using static Exa.Input.GameControls;
using System.Windows;

namespace Exa.Grids.Blueprints.Editor
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

        private EventRef blueprintNameEditEventRef;
        private EventRef saveButtonOnClickEventRef;

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

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(() =>
            {
                IsSaved = false;
            });

            stopwatch.onTime.AddListener(ValidateGrid);
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
            Zoom = 5f;
            Camera.main.orthographicSize = Zoom;
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

            editorGrid.Import(newBlueprint);
            editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.text = newBlueprint.name;

            SetCallbacks(blueprintContainer, saveCallback);
            ValidateName(blueprintContainer, newBlueprint.name);

            IsSaved = true;
        }

        public void SetCallbacks(ObservableBlueprint blueprintContainer, Action<ObservableBlueprint> saveCallback)
        {
            // Set blueprint name input field callback
            var onValueChanged = editorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged;
            onValueChanged.AddListenerOnce((value) =>
            {
                ValidateName(blueprintContainer, value);
                IsSaved = false;
            }, blueprintNameEditEventRef);

            // Set save button callback
            var onClick = editorOverlay.blueprintInfoPanel.saveButton.onClick;
            onClick.AddListenerOnce(() => 
            {
                ValidateAndSave(blueprintContainer, saveCallback);
            }, saveButtonOnClickEventRef);
        }

        public void ValidateGrid()
        {
            var args = new BlueprintGridValidationArgs
            {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.Blocks
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

        public void ExportToClipboard()
        {
            var json = IOUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }

        private void SetGridBackground()
        {
            var screenHeightInUnits = Camera.main.orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
            editorGridBackground.transform.localScale = new Vector3(screenWidthInUnits, screenHeightInUnits);
        }
    }
}