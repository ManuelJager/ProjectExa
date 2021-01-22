namespace Exa.ShipEditor
{
    public partial class GridEditor
    {
        private BlueprintGridValidator gridValidator;
        private BlueprintNameValidator nameValidator;

        public void ValidateGrid() {
            var args = new BlueprintGridValidationArgs {
                blueprintBlocks = editorGrid.blueprintLayer.ActiveBlueprint.Blocks
            };

            GridValidationResult = overlay
                .infoPanel
                .errorListController
                .Validate(gridValidator, args);
        }

        public void ValidateName(string name) {
            var args = new BlueprintNameValidationArgs {
                collectionContext = Systems.Blueprints.userBlueprints,
                requestedName = name,
                blueprintContainer = container
            };

            NameValidationResult = overlay.infoPanel.errorListController
                .Validate(nameValidator, args);

            if (NameValidationResult) {
                editorGrid.blueprintLayer.ActiveBlueprint.name = name;
            }
        }
    }
}