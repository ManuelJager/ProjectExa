﻿#pragma warning disable CS0649

using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.Editor;
using Exa.IO;
using Exa.UI.Components;
using System.Collections;
using UnityEngine;

namespace Exa.UI
{
    internal class BlueprintViewController : ViewController<BlueprintView, BlueprintContainer, Blueprint>, IUIGroup
    {
        public BlueprintContainerCollection collectionContext;
        public ShipEditor shipEditor;

        [SerializeField] private ReturnNavigateable shipEditorBlueprintSelector;
        [SerializeField] private Navigateable shipEditorNavigateable;
        [SerializeField] private BlueprintDetails blueprintDetails;

        private bool interactible = true;

        public bool Interactable
        {
            get => interactible;
            set
            {
                interactible = value;

                shipEditorBlueprintSelector.Interactable = value;
            }
        }

        public void OnAddNewBlueprint()
        {
            Systems.MainUI.promptController.PromptForm(
                message: "Add blueprint",
                uiGroup: this,
                modelDescriptor: new BlueprintCreationOptionsDescriptor(),
                onSubmit: ImportBlueprintWithOptions);
        }

        public void OnImportBlueprintFromClipboard()
        {
            Blueprint blueprint;

            var clipboardText = GUIUtility.systemCopyBuffer;

            if (string.IsNullOrEmpty(clipboardText))
            {
                UserExceptionLogger.Instance.Log("Clipboard is empty");
                return;
            }

            try
            {
                blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(clipboardText);
            }
            catch
            {
                UserExceptionLogger.Instance.Log("Clipboard data is formatted incorrectly");
                return;
            }

            var container = new BlueprintContainer(blueprint);

            if (Source.Contains(container))
            {
                UserExceptionLogger.Instance.Log("Blueprint with given name already added");
                return;
            }

            // Save blueprint and generate thumbnail
            StartCoroutine(TrySaveAndImport(container));
        }

        public override void ViewCreation(BlueprintView view, BlueprintContainer container)
        {
            view.button.onClick.AddListener(() =>
            {
                if (!Interactable) return;

                shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
                shipEditor.Import(container, TrySave);
            });
            view.deleteButton.onClick.AddListener(() =>
            {
                if (!Interactable) return;

                Systems.MainUI.promptController.PromptYesNo("Are you sure you want to delete this blueprint?", this, (yes) =>
                {
                    if (yes)
                    {
                        Source.Remove(container);
                    }
                });
            });
            view.hoverable.onPointerEnter.AddListener(() =>
            {
                blueprintDetails.Reflect(container.Data);
            });
            view.hoverable.onPointerExit.AddListener(() =>
            {
                blueprintDetails.Reflect(null);
            });
        }

        public IEnumerator TrySave(BlueprintContainer container)
        {
            if (!Source.Contains(container))
            {
                Source.Add(container);
            }

            yield return Systems.ThumbnailGenerator.GenerateThumbnail(container.Data);
            container.ThumbnailFileHandle.Refresh();
            container.BlueprintFileHandle.Refresh();
        }

        private void ImportBlueprintWithOptions(BlueprintCreationOptions options)
        {
            var observableBlueprint = new BlueprintContainer(new Blueprint(options));

            shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
            shipEditor.Import(observableBlueprint, TrySave);
        }

        private IEnumerator TrySaveAndImport(BlueprintContainer container)
        {
            yield return TrySave(container);

            // Navigate to editor and import blueprint
            shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
            shipEditor.Import(container, TrySave);
        }
    }
}