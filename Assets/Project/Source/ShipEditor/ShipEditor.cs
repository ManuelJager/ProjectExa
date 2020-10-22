using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using System;
using Exa.UI.Tooltips;
using UnityEngine;
using UnityEngine.Serialization;
using static Exa.Input.GameControls;
#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    [RequireComponent(typeof(ShipEditorNavigateable))]
    public partial class ShipEditor : MonoBehaviour, IEditorActions, IUIGroup
    {
        public EditorGrid editorGrid;
        public ShipEditorNavigateable navigateable;

        [SerializeField] private ShipEditorStopwatch stopwatch;
        [SerializeField] private GameObject editorGridBackground;
        [SerializeField] private float zoomSpeed;
        private GameControls gameControls;
        private ShipEditorOverlay overlay;

        private void Awake()
        {
            overlay = Systems.UI.editorOverlay;

            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);

            overlay.infoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            overlay.inventory.blockSelected.AddListener(editorGrid.OnBlockSelected);
            overlay.onPointerEnter.AddListener(OnOverlayPointerEnter);
            overlay.onPointerExit.AddListener(OnOverlayPointerExit);
            overlay.infoPanel.blueprintNameInput.inputField.onValueChanged.AddListener(OnBlueprintNameInputChanged);
            overlay.infoPanel.saveButton.onClick.AddListener(OnBlueprintSave);

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(OnBlueprintChanged);

            stopwatch.onTime.AddListener(OnBlueprintGridValidationRequested);

            SetGridBackground();
        }

        private void OnEnable()
        {
            ResetState();

            overlay.gameObject.SetActive(true);
            gameControls.Enable();
        }

        private void OnDisable()
        {
            overlay.gameObject.SetActive(false);
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

            overlay.infoPanel.blueprintNameInput.SetValue(newBlueprint.name, false);
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