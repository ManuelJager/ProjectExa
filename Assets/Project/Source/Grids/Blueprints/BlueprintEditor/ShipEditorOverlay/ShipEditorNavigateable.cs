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
            if (!Interactible) return;

            var blueprintIsSaved = shipEditor.IsSaved;
            if (!blueprintIsSaved)
            {
                GameManager.Instance.promptController.PromptYesNo("Are you sure you want to exit without saving?", this, (yes) =>
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