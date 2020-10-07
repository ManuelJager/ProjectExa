using Exa.UI;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.ShipEditor
{
    [RequireComponent(typeof(ShipEditor))]
    public class ShipEditorNavigateable : ReturnNavigateable
    {
        [SerializeField] private ShipEditor _shipEditor;

        protected override void Return(bool force = false)
        {
            if (!Interactable) return;

            if (!_shipEditor.IsSaved)
            {
                Systems.Ui.promptController.PromptYesNo("Are you sure you want to exit without saving?", _shipEditor, (yes) =>
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