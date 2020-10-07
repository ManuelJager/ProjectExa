namespace Exa.ShipEditor
{
    public partial class ShipEditor
    {
        public void OnBlueprintClear()
        {
            // Hide block ghost and ask user for blueprint clear confirmation
            Systems.Ui.promptController.PromptYesNo("Are you sure you want to clear the blueprint?", this, (yes) =>
            {
                if (yes)
                {
                    IsSaved = false;
                    editorGrid.ClearBlueprint();
                }
            });
        }

        public void OnOverlayPointerEnter()
        {
            MouseOverUi = true;
        }

        public void OnOverlayPointerExit()
        {
            MouseOverUi = false;
        }

        public void OnBlueprintChanged()
        {
            IsSaved = false;
            UpdateSaveButtonActive();
        }

        public void OnBlueprintNameInputChanged(string value)
        {
            IsSaved = false;
            ValidateName(value);
            UpdateSaveButtonActive();
        }

        public void OnBlueprintGridValidationRequested()
        {
            ValidateGrid();
            UpdateSaveButtonActive();
        }

        public void OnBlueprintSave()
        {
            // Don't to save twice
            if (IsSaved) return;

            // Make sure the grid is validated before saving
            ValidateGrid();
            UpdateSaveButtonActive();

            if (!ShouldSave) return;

            IsSaved = true;
            UpdateSaveButtonActive();

            // Set the value of the observable
            _container.SetData(editorGrid.blueprintLayer.ActiveBlueprint, false);

            // Save the blueprint, generate the thumbnail
            _saveCallback(_container);

            // Notify after saving as observers require the thumbnail to be generated
            _container.Notify();
        }
    }
}