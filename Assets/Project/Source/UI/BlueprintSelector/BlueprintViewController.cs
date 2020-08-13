#pragma warning disable CS0649

using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.IO;
using Exa.UI.Components;
using System.Collections;
using UnityEngine;

namespace Exa.UI
{
    internal class BlueprintViewController : ViewController<BlueprintView, BlueprintContainer, Blueprint>, IUIGroup
    {
        public BlueprintContainerCollection collectionContext;
        public ShipEditor.ShipEditor shipEditor;

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
            Systems.UI.promptController.PromptForm(
                message: "Add blueprint",
                uiGroup: this,
                modelDescriptor: new BlueprintOptionsDescriptor(),
                onSubmit: ImportBlueprintWithOptions);
        }

        #region Clipboard import

        public void OnClipboardImport()
        {
            var clipboardText = GUIUtility.systemCopyBuffer;

            if (string.IsNullOrEmpty(clipboardText))
            {
                UserExceptionLogger.Instance.Log("Clipboard is empty");
                return;
            }

            if (!OnClipboardImportDeserialize(clipboardText, out var blueprint)) return;

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            if (Systems.Blueprints.ContainsName(blueprint.name))
            {
                UserExceptionLogger.Instance.Log("Blueprint with given name already added");
                return;
            }

            // Save blueprint and generate thumbnail
            TrySave(container);
        }

        private bool OnClipboardImportDeserialize(string json, out Blueprint blueprint)
        {
            try
            {
                blueprint = IOUtils.JsonDeserializeWithSettings<Blueprint>(json);
                return true;
            }
            catch
            {
                UserExceptionLogger.Instance.Log("Clipboard data is formatted incorrectly");
                blueprint = null;
                return false;
            }
        }

        #endregion

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

                Systems.UI.promptController.PromptYesNo("Are you sure you want to delete this blueprint?", this, (yes) =>
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

        public void TrySave(BlueprintContainer container)
        {
            Systems.Thumbnails.GenerateThumbnail(container.Data);
            container.ThumbnailFileHandle.Refresh();
            container.BlueprintFileHandle.Refresh();

            if (!Source.Contains(container))
            {
                Source.Add(container);
            }
        }

        private void ImportBlueprintWithOptions(BlueprintOptions options)
        {
            var blueprint = new Blueprint(options);

            if (Systems.Blueprints.ContainsName(blueprint.name))
            {
                Systems.UI.userExceptionLogger.Log($"Blueprint name \"{blueprint.name}\" is already used");
                return;
            }

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
            shipEditor.Import(container, TrySave);
        }
    }
}