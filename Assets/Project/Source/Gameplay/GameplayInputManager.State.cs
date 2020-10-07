namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private IRaycastTarget raycastTarget = null;
        private ShipSelection currentSelection;

        private bool HasSelection
        {
            get => currentSelection != null;
        }

        private bool IsSelectingArea
        {
            get => selectionBuilder != null;
        }

        public ShipSelection CurrentSelection
        {
            get => currentSelection;
            set
            {
                currentSelection = value;
                GameSystems.UI.selectionOverlay.Reflect(value);
            }
        }
    }
}