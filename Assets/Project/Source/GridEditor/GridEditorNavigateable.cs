using Exa.UI.Components;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    [RequireComponent(typeof(GridEditor))]
    public class GridEditorNavigateable : ReturnNavigateable
    {
        [SerializeField] private GridEditor gridEditor;

        protected override void Return(bool force = false) {
            if (!Interactable) return;

            var confirmationString = "Are you sure you want to exit without saving?";
            if (!gridEditor.IsSaved) {
                Systems.UI.promptController.PromptYesNo(confirmationString, gridEditor, OnClosePrompt);
            }
            else {
                base.Return();
            }
        }

        private void OnClosePrompt(bool confirm) {
            if (confirm) {
                base.Return(true);
            }
        }
    }
}