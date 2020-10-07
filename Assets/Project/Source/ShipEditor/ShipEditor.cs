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
    public partial class ShipEditor : MonoBehaviour, IEditorActions, IUiGroup
    {
        [HideInInspector] public BlueprintContainerCollection blueprintCollection;
        public EditorGrid editorGrid;
        public ShipEditorNavigateable navigateable;

        [SerializeField] private ShipEditorStopwatch _stopwatch;
        [SerializeField] private GameObject _editorGridBackground;
        [SerializeField] private float _zoomSpeed;
        private GameControls _gameControls;
        private ShipEditorOverlay _shipEditorOverlay;

        private void Awake()
        {
            _shipEditorOverlay = Systems.Ui.editorOverlay;

            _gameControls = new GameControls();
            _gameControls.Editor.SetCallbacks(this);

            _shipEditorOverlay.blueprintInfoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            _shipEditorOverlay.inventory.blockSelected.AddListener(editorGrid.OnBlockSelected);
            _shipEditorOverlay.onPointerEnter.AddListener(OnOverlayPointerEnter);
            _shipEditorOverlay.onPointerExit.AddListener(OnOverlayPointerExit);
            _shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.inputField.onValueChanged.AddListener(OnBlueprintNameInputChanged);
            _shipEditorOverlay.blueprintInfoPanel.saveButton.onClick.AddListener(OnBlueprintSave);

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(OnBlueprintChanged);

            _stopwatch.onTime.AddListener(OnBlueprintGridValidationRequested);

            SetGridBackground();
        }

        private void OnEnable()
        {
            ResetState();

            _shipEditorOverlay.gameObject.SetActive(true);
            _gameControls.Enable();
        }

        private void OnDisable()
        {
            _shipEditorOverlay.gameObject.SetActive(false);
            _gameControls.Disable();
        }

        private void Update()
        {
            if (_leftButtonPressed)
            {
                editorGrid.OnLeftClickPressed();
                return;
            }

            if (_rightButtonPressed)
            {
                editorGrid.OnRightClickPressed();
                return;
            }
        }

        public void Import(BlueprintContainer blueprintContainer, Action<BlueprintContainer> saveCallback)
        {
            this._container = blueprintContainer;
            this._saveCallback = saveCallback;
            this._gridValidator = new BlueprintGridValidator();
            this._nameValidator = new BlueprintNameValidator();

            var newBlueprint = blueprintContainer.Data.Clone();
            editorGrid.Import(newBlueprint);

            ValidateName(newBlueprint.name);

            _shipEditorOverlay.blueprintInfoPanel.blueprintNameInput.SetValueWithoutNotify(newBlueprint.name);
        }

        public void ExportToClipboard()
        {
            var json = IoUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }

        private void SetGridBackground()
        {
            var screenHeightInUnits = Camera.main.orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
            _editorGridBackground.transform.localScale = new Vector3(screenWidthInUnits, screenHeightInUnits);
        }
    }
}