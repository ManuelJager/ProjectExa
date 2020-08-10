namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private IRaycastTarget raycastTarget = null;
        private ShipSelection shipSelection;

        private bool HasSelection
        {
            get => shipSelection != null;
        }

        private bool IsSelectingArea
        {
            get => selectionBuilder != null;
        }

        public ShipSelection ShipSelection
        {
            get => shipSelection;
            set
            {
                shipSelection = value;
                GameSystems.UI.selectionOverlay.Reflect(value);
            }
        }
    }
}