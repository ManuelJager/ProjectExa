namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        private BlueprintGridValidator _gridValidator;
        private BlueprintNameValidator _nameValidator;

        public void ValidateGrid()
        {
            var args = new BlueprintGridValidationArgs
            {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.Blocks
            };

            GridValidationResult = _shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(_gridValidator, args);
        }

        public void ValidateName(string name)
        {
            var args = new BlueprintNameValidationArgs
            {
                collectionContext = blueprintCollection,
                requestedName = name,
                blueprintContainer = _container
            };

            NameValidationResult = _shipEditorOverlay
                .blueprintInfoPanel
                .errorListController
                .Validate(_nameValidator, args);

            if (NameValidationResult)
            {
                editorGrid.blueprintLayer.ActiveBlueprint.name = name;
            }
        }
    }
}