using Exa.Bindings;
using Exa.Generics;
using Exa.Math;
using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay
{
    public abstract class ShipSelection : ObservableCollection<Ship>, ICloneable<ShipSelection>
    {
        protected Formation formation;

        protected ShipSelection(Formation formation) {
            this.formation = formation;
        }

        public bool CanControl { get; protected set; }

        public Vector2 AveragePosition {
            get {
                var positions = this.Select(ship => ship.gameObject.transform.position.ToVector2());

                return MathUtils.Average(positions);
            }
        }

        private readonly Dictionary<Ship, UnityAction> callbackDict = new Dictionary<Ship, UnityAction>();

        public override void Add(Ship ship) {
            base.Add(ship);

            ship.Overlay.overlayCircle.IsSelected = true;

            // Set a callback that removes the Ship from the collection when destroyed
            void Callback() => Remove(ship);
            callbackDict.Add(ship, Callback);
            ship.ControllerDestroyedEvent.AddListener(Callback);
        }

        public override bool Remove(Ship ship) {
            OnRemove(ship);
            return base.Remove(ship);
        }

        public override void Clear() {
            foreach (var ship in this) {
                OnRemove(ship);
            }

            base.Clear();
        }

        private void OnRemove(Ship ship) {
            ship.Overlay.overlayCircle.IsSelected = false;

            // Get the callback and remove it
            var callback = callbackDict[ship];
            callbackDict.Remove(ship);
            ship.ControllerDestroyedEvent.RemoveListener(callback);
        }

        public abstract ShipSelection Clone();
    }
}