using Exa.Ships;
using Exa.Generics;
using UnityEngine;

namespace Exa.Gameplay
{
    public class SelectionBuilder : IBuilder<ShipSelection>
    {
        private readonly Vector2 _startSelectionPos;
        private ShipSelection _selection;

        public SelectionBuilder(Vector2 startSelectionPos)
        {
            this._startSelectionPos = startSelectionPos;
        }

        public void UpdateSelection(Vector2 endSelectionPos)
        {
            var layerMask = LayerMask.GetMask("unit");
            var colliders = Physics2D.OverlapAreaAll(_startSelectionPos, endSelectionPos, layerMask);

            foreach (var collider in colliders)
            {
                var ship = collider.gameObject.GetComponent<Ship>();

                if (_selection == null)
                {
                    _selection = ship.GetAppropriateSelection(new VicFormation());
                }

                if (ship != null && !_selection.Contains(ship) && ship.MatchesSelection(_selection))
                {
                    _selection.Add(ship);
                }
            }
        }

        public ShipSelection Build()
        {
            return _selection;
        }
    }
}