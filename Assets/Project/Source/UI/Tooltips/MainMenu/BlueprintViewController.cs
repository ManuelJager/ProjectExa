#pragma warning disable CS0649

using Exa.Bindings;
using Exa.Grids.Blueprints;
using Exa.Grids.Blueprints.BlueprintEditor;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.UI
{
    internal class BlueprintViewController : ViewController<BlueprintView, ObservableBlueprint, Blueprint>, IInteractableGroup
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
            GameManager.Instance.promptController.PromptForm(
                "Add blueprint",
                this,
                new BlueprintCreationOptionsDescriptor(),
                (options) =>
            {
                var observableBlueprint = new ObservableBlueprint(new Blueprint(options));

                shipEditorBlueprintSelector.NavigateTo(shipEditorNavigateable);
                shipEditor.Import(observableBlueprint, TrySave);
            });
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

                GameManager.Instance.promptController.PromptYesNo("Are you sure you want to delete this blueprint?", this, (yes) =>
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
                GameManager.Instance.blueprintManager.Save();
            }
        }
    }
}