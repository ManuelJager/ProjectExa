using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using System;
using UnityEngine;
using static Exa.Input.GameControls;

namespace Exa.ShipEditor
{
    [RequireComponent(typeof(ShipEditorNavigateable))]
    public partial class ShipEditor : MonoBehaviour, IEditorActions, IUIGroup
    {
        [HideInInspector] public BlueprintContainerCollection blueprintCollection;
        public EditorGrid editorGrid;
        public ShipEditorNavigateable navigateable;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        [SerializeField] private GameObject editorGridBackground;
        [SerializeField] private float zoomSpeed;
        private GameControls gameControls;
        private ShipEditorOverlay shipEditorOverlay;

        private void Awake()
        {
            shipEditorOverlay = Systems.UI.editorOverlay;

            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);

            shipEditorOverlay.blueprintInfoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            shipEditorOverlay.inventory.blockSelected.AddListener(editorGrid.OnBlockSelected);
            shipEditorOverlay.onPointerEnter.AddListener(OnOverlayPointerEnter);
            shipEditorOverlay.onPointerExit.AddListener(OnOverlayPointerExit);
            shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged.AddListener(OnBlueprintNameInputChanged);
            shipEditorOverlay.blueprintInfoPanel.saveButton.onClick.AddListener(OnBlueprintSave);

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(OnBlueprintChanged);

            stopwatch.onTime.AddListener(OnBlueprintGridValidationRequested);

            SetGridBackground();
        }

        private void OnEnable()
        {
            ResetState();

            shipEditorOverlay.gameObject.SetActive(true);
            gameControls.Enable();
        }

        private void OnDisable()
        {
            shipEditorOverlay.gameObject.SetActive(false);
            gameControls.Disable();
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

        public void Import(BlueprintContainer blueprintContainer, Action<BlueprintContainer> saveCallback)
        {
            this.container = blueprintContainer;
            this.saveCallback = saveCallback;
            this.gridValidator = new BlueprintGridValidator();
            this.nameValidator = new BlueprintNameValidator();

            var newBlueprint = blueprintContainer.Data.Clone();
            editorGrid.Import(newBlueprint);

            ValidateName(newBlueprint.name);

            shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.SetValueWithoutNotify(newBlueprint.name);
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