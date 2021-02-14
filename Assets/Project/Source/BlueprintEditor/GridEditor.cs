using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using System;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.UI.Controls;
using Exa.Utils;
using UnityEngine;
using static Exa.Input.GameControls;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    [RequireComponent(typeof(GridEditorNavigateable))]
    public partial class GridEditor : MonoBehaviour, IEditorActions, IUIGroup
    {
        public EditorGrid editorGrid;
        public GridEditorNavigateable navigateable;

        [SerializeField] private GridEditorStopwatch stopwatch;
        [SerializeField] private GameObject editorGridBackground;
        [SerializeField] private float zoomSpeed;
        private GameControls gameControls;
        private ShipEditorOverlay overlay;

        public BlockContext Context { get; private set; }

        private void Awake() {
            overlay = Systems.UI.EditorOverlay;

            gameControls = new GameControls();
            gameControls.Editor.SetCallbacks(this);

            overlay.infoPanel.clearButton.onClick.AddListener(OnBlueprintClear);
            overlay.inventory.blockSelected.AddListener(editorGrid.OnBlockSelected);
            overlay.inventory.Init();
            overlay.onPointerEnter.AddListener(OnOverlayPointerEnter);
            overlay.onPointerExit.AddListener(OnOverlayPointerExit);
            overlay.infoPanel.blueprintNameInput.inputField.onValueChanged.AddListener(OnBlueprintNameInputChanged);
            overlay.infoPanel.saveButton.onClick.AddListener(OnBlueprintSave);

            editorGrid.blueprintLayer.onBlueprintChanged.AddListener(OnBlueprintChanged);

            stopwatch.onTime.AddListener(OnBlueprintGridValidationRequested);
            
            #if UNITY_EDITOR
            var button = ButtonControl.Create(overlay.infoPanel.controlsContainer, "Save as asset");
            button.OnClick.AddListener(() => {
                var blueprint = editorGrid.blueprintLayer.ActiveBlueprint;
                var staticBlueprint = ScriptableObject.CreateInstance<StaticBlueprint>();
                staticBlueprint.Save(blueprint);
            });
            button.LayoutElement.preferredHeight = 32;
            #endif

            SetGridBackground();
        }

        private void OnEnable() {
            overlay.gameObject.SetActive(true);
            gameControls.Enable();
        }

        private void OnDisable() {
            overlay.gameObject.SetActive(false);
            gameControls.Disable();
        }

        private void Update() {
            if (leftButtonPressed) {
                editorGrid.OnLeftClickPressed();
                return;
            }

            if (rightButtonPressed) {
                editorGrid.OnRightClickPressed();
                return;
            }
        }

        public void Import(BlueprintContainer blueprintContainer, BlockContext context, Action<BlueprintContainer> saveCallback) {
            ResetState();

            editorGrid.turretLayer.Init();
            var templates = Systems.Blocks.blockTemplates.SelectNonNull(elem => elem.Data as ITurretTemplate);
            foreach (var template in templates) {
                editorGrid.turretLayer.GenerateTurretOverlayPrefab(template);
            }

            this.container = blueprintContainer;
            this.saveCallback = saveCallback;
            this.gridValidator = new BlueprintGridValidator();
            this.nameValidator = new BlueprintNameValidator();
            this.Context = context;

            var newBlueprint = blueprintContainer.Data.Clone();
            editorGrid.Import(newBlueprint, () => {
                ValidateName(newBlueprint.name);
            });

            overlay.infoPanel.blueprintNameInput.SetValue(newBlueprint.name, false);
            overlay.inventory.SetFilter(blueprintContainer.Data.BlueprintType.allowedBlockCategory);
            overlay.inventory.SetSelected(null);
            overlay.inventory.CloseTabs();

            FlipState = BlockFlip.None;
        }

        public void ExportToClipboard() {
            var json = IOUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }

        private void SetGridBackground() {
            var screenHeightInUnits = Camera.main.orthographicSize * 2;
            var screenWidthInUnits = screenHeightInUnits * Screen.width / Screen.height;
            editorGridBackground.transform.localScale = new Vector3(screenWidthInUnits, screenHeightInUnits);
        }
    }
}