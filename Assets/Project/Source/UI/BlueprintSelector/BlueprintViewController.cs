#pragma warning disable CS0649

using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.Editor;
using Exa.IO;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI
{
    internal class BlueprintViewController : ViewController<BlueprintView, ObservableBlueprint, Blueprint>, IUIGroup
    {
        public ObservableBlueprintCollection collectionContext;
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

            var observableBlueprint = new ObservableBlueprint(blueprint);

            if (Source.Contains(observableBlueprint))
            {
                UserExceptionLogger.Instance.Log("Blueprint with given name already added");
                return;
            }

            // Save blueprint and generate thumbnail
            TrySave(observableBlueprint);

            // Navigate to editor and import blueprint
            shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
            shipEditor.Import(observableBlueprint, TrySave);
        }

        public override void ViewCreation(BlueprintView view, ObservableBlueprint observer)
        {
            view.button.onClick.AddListener(() =>
            {
                if (!Interactable) return;

                shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
                shipEditor.Import(observer, TrySave);
            });
            view.deleteButton.onClick.AddListener(() =>
            {
                if (!Interactable) return;

                Systems.MainUI.promptController.PromptYesNo("Are you sure you want to delete this blueprint?", this, (yes) =>
                {
                    if (yes)
                    {
                        Source.Remove(observer);
                    }
                });
            });
            view.hoverable.onPointerEnter.AddListener(() =>
            {
                blueprintDetails.Reflect(observer.Data);
            });
            view.hoverable.onPointerExit.AddListener(() =>
            {
                blueprintDetails.Reflect(null);
            });
        }

        public void TrySave(ObservableBlueprint observableBlueprint)
        {
            if (!Source.Contains(observableBlueprint))
            {
                Source.Add(observableBlueprint);
            }
            if (observableBlueprint.Data.Blocks != null)
            {
                observableBlueprint.GenerateThumbnail();
                observableBlueprint.BlueprintFileHandle.UpdatePath();
            }
        }

        private void ImportBlueprintWithOptions(BlueprintCreationOptions options)
        {
            var observableBlueprint = new ObservableBlueprint(new Blueprint(options));

            shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
            shipEditor.Import(observableBlueprint, TrySave);
        }
    }
}