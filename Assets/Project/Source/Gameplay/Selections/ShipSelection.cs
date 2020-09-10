using Exa.Bindings;
using Exa.Generics;
using Exa.Math;
using Exa.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay
{
    public abstract class ShipSelection : ObservableCollection<Ship>, ICloneable<ShipSelection>
    {
        protected Formation formation;

        public ShipSelection(Formation formation)
        {
            this.formation = formation;
        }

        public bool CanControl { get; protected set; }
        public Vector2 AveragePosition
        {
            get
            {
                var positions = this.Select((ship) => 
                {
                    return ship.gameObject.transform.position.ToVector2();
                });

                return MathUtils.Average(positions);
            }
        }

        private readonly Dictionary<Ship, UnityAction> callbackDict = new Dictionary<Ship, UnityAction>();

        public override void Add(Ship ship)
        {
            base.Add(ship);

            ship.overlay.overlayCircle.IsSelected = true;

            // Set a callback that removes the Ship from the collection when destroyed
            UnityAction callback = () => Remove(ship);
            callbackDict.Add(ship, callback);
            ship.destroyEvent.AddListener(callback);
        }

        public override bool Remove(Ship ship)
        {
            OnRemove(ship);
            return base.Remove(ship);
        }

        public override void Clear()
        {
            foreach (var ship in this)
            {
                OnRemove(ship);
            }

            base.Clear();
        }

        private void OnRemove(Ship ship)
        {
            ship.overlay.overlayCircle.IsSelected = false;

            // Get the callback and remove it
            var callback = callbackDict[ship];
            callbackDict.Remove(ship);
            ship.destroyEvent.RemoveListener(callback);
        }

        public abstract ShipSelection Clone();
    }
}