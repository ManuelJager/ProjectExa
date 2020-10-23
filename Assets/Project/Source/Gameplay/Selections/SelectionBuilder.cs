using Exa.Ships;
using Exa.Generics;
using UnityEngine;

namespace Exa.Gameplay
{
    public class SelectionBuilder : IBuilder<ShipSelection>
    {
        private readonly Vector2 startSelectionPos;
        private ShipSelection selection;

        public SelectionBuilder(Vector2 startSelectionPos) {
            this.startSelectionPos = startSelectionPos;
        }

        public void UpdateSelection(Vector2 endSelectionPos) {
            var layerMask = LayerMask.GetMask("unit");
            var colliders = Physics2D.OverlapAreaAll(startSelectionPos, endSelectionPos, layerMask);

            foreach (var collider in colliders) {
                var ship = collider.gameObject.GetComponent<Ship>();

                if (selection == null) {
                    selection = ship.GetAppropriateSelection(new VicFormation());
                }

                if (ship != null && !selection.Contains(ship) && ship.MatchesSelection(selection)) {
                    selection.Add(ship);
                }
            }
        }

        public ShipSelection Build() {
            return selection;
        }
    }
}