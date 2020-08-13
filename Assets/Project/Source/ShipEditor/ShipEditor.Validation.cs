﻿namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        public void ValidateGrid()
        {
            var args = new BlueprintGridValidationArgs
            {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.Blocks
            };

            GridValidationResult = shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintGridValidator(), args);
        }

        public void ValidateName(string name)
        {
            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = name,
                blueprintContainer = container
            };

            NameValidationResult = shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(new BlueprintNameValidator(), args);

            if (NameValidationResult)
            {
                editorGrid.blueprintLayer.ActiveBlueprint.name = name;
            }
        }
    }
}