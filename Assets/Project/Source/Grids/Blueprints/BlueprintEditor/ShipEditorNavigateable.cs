using Exa.UI.Components;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    [RequireComponent(typeof(ShipEditor))]
    public class ShipEditorNavigateable : ReturnNavigateable
    {
        private ShipEditor shipEditor;

        protected override void Awake()
        {
            base.Awake();
            shipEditor = GetComponent<ShipEditor>();
        }

        protected override void Return(bool force = false)
        {
            if (!Interactable) return;

            var blueprintIsSaved = shipEditor.IsSaved;
            if (!blueprintIsSaved)
            {
                GameManager.Instance.promptController.PromptYesNo("Are you sure you want to exit without saving?", shipEditor, (yes) =>
                {
                    if (yes)
                    {
                        base.Return(true);
                    }
                });
            }
            else
            {
                base.Return();
            }
        }
    }
}