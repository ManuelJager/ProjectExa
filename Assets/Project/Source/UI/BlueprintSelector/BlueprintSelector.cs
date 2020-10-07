#pragma warning disable CS0649

using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.IO;
using Exa.UI.Components;
using System.Collections;
using UnityEngine;

namespace Exa.UI
{
    public class BlueprintSelector : ViewController<BlueprintView, BlueprintContainer, Blueprint>, IUiGroup
    {
        public BlueprintContainerCollection collectionContext;
        public ShipEditor.ShipEditor shipEditor;

        [SerializeField] private Navigateable _blueprintSelectorNavigateable;
        [SerializeField] private Navigateable _shipEditorNavigateable;
        [SerializeField] private BlueprintDetails _blueprintDetails;

        private bool _interactible = true;

        public bool Interactable
        {
            get => _interactible;
            set
            {
                _interactible = value;

                _blueprintSelectorNavigateable.Interactable = value;
            }
        }

        public void OnAddNewBlueprint()
        {
            Systems.Ui.promptController.PromptForm(
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
                Systems.Ui.logger.Log("Clipboard is empty");
                return;
            }

            if (!OnClipboardImportDeserialize(clipboardText, out var blueprint)) return;

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            if (Systems.Blueprints.ContainsName(blueprint.name))
            {
                Systems.Ui.logger.Log("Blueprint with given name already added");
                return;
            }

            // Save blueprint and generate thumbnail
            TrySave(container);
        }

        private bool OnClipboardImportDeserialize(string json, out Blueprint blueprint)
        {
            try
            {
                blueprint = IoUtils.JsonDeserializeWithSettings<Blueprint>(json);
                return true;
            }
            catch
            {
                Systems.Ui.logger.Log("Clipboard data is formatted incorrectly");
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

                _blueprintSelectorNavigateable.NavigateTo(_shipEditorNavigateable, new NavigationArgs
                {
                    current = _blueprintSelectorNavigateable
                });
                shipEditor.Import(container, TrySave);
            });
            view.deleteButton.onClick.AddListener(() =>
            {
                if (!Interactable) return;

                Systems.Ui.promptController.PromptYesNo("Are you sure you want to delete this blueprint?", this, (yes) =>
                {
                    if (yes)
                    {
                        Source.Remove(container);
                    }
                });
            });
            view.hoverable.onPointerEnter.AddListener(() =>
            {
                _blueprintDetails.Reflect(container.Data);
            });
            view.hoverable.onPointerExit.AddListener(() =>
            {
                _blueprintDetails.Reflect(null);
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
                Systems.Ui.logger.Log($"Blueprint name \"{blueprint.name}\" is already used");
                return;
            }

            var args = new BlueprintContainerArgs(blueprint);
            var container = new BlueprintContainer(args);

            _blueprintSelectorNavigateable.NavigateTo(_shipEditorNavigateable);
            shipEditor.Import(container, TrySave);
        }
    }
}