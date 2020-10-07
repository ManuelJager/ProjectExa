namespace Exa.Gameplay
{
    public partial class GameplayInputManager
    {
        private IRaycastTarget _raycastTarget = null;
        private ShipSelection _currentSelection;

        private bool HasSelection
        {
            get => _currentSelection != null;
        }

        private bool IsSelectingArea
        {
            get => _selectionBuilder != null;
        }

        public ShipSelection CurrentSelection
        {
            get => _currentSelection;
            set
            {
                _currentSelection = value;
                GameSystems.Ui.selectionOverlay.Reflect(value);
            }
        }
    }
}