﻿using Exa.UI;
using Exa.UI.Components;
using UnityEngine;

namespace Exa.Grids.Blueprints.Editor
{
    [RequireComponent(typeof(ShipEditor))]
    public class ShipEditorNavigateable : ReturnNavigateable
    {
        [SerializeField] private ShipEditor shipEditor;

        protected override void Return(bool force = false)
        {
            if (!Interactable) return;

            if (!shipEditor.IsSaved)
            {
                Systems.UI.promptController.PromptYesNo("Are you sure you want to exit without saving?", shipEditor, (yes) =>
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