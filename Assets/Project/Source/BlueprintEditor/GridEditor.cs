﻿using Exa.Grids.Blueprints;
using Exa.Input;
using Exa.IO;
using Exa.UI;
using System.Collections.Generic;
using Exa.Camera;
using Exa.UI.Controls;
using Exa.Utils;
using Exa.Validation;
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
        [SerializeField] private float zoomSpeed;
        private GameControls gameControls;
        private ShipEditorOverlay overlay;
        private EditorCameraTarget editorCameraTarget;

        private void Awake() {
            overlay = Systems.UI.EditorOverlay;
            editorCameraTarget = new EditorCameraTarget(new CameraTargetSettings());

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
        }

        private void OnEnable() {
            overlay.gameObject.SetActive(true);
            gameControls.Enable();
            
            Systems.CameraController.SetTarget(editorCameraTarget, true);
        }

        private void OnDisable() {
            overlay.gameObject.SetActive(false);
            gameControls.Disable();

            customValidators.Values.ForEach(cache => cache.cleanUp());
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

        public void Import(GridEditorImportArgs importArgs) {
            ImportArgs = importArgs;
            customValidators = new Dictionary<IValidator, CustomValidatorCache>();

            BaseImport(importArgs.GetBlueprint(), importArgs.ValidateName);

            if (!ImportArgs.CustomValidators.IsNotNull(out var value)) return;
            
            foreach (var customValidator in value) {
                var cache = new CustomValidatorCache();

                var (validator, cleanUp) = customValidator((errors) => { cache.result = errors; });

                cache.cleanUp = cleanUp;

                customValidators.Add(validator, cache);
            }
        }

        private void BaseImport(Blueprint blueprint, bool enableNameChanging) {
            ResetState();

            editorGrid.turretLayer.Init();

            gridValidator = new BlueprintGridValidator();
            nameValidator = enableNameChanging ? new BlueprintNameValidator() : null;
            overlay.infoPanel.SetNameEditingActive(enableNameChanging);

            var newBlueprint = blueprint.Clone();
            editorGrid.Import(newBlueprint, () => {
                if (enableNameChanging) {
                    ValidateName(newBlueprint.name);
                }
            });

            overlay.infoPanel.blueprintNameInput.SetValue(newBlueprint.name, false);
            overlay.inventory.SetFilter(blueprint.BlueprintType.allowedBlockCategory);
            overlay.inventory.SetSelected(null);
            overlay.inventory.CloseTabs();

            FlipState = BlockFlip.None;
        }

        public void ExportToClipboard() {
            var json = IOUtils.JsonSerializeWithSettings(editorGrid.blueprintLayer.ActiveBlueprint);
            GUIUtility.systemCopyBuffer = json;
        }
    }
}