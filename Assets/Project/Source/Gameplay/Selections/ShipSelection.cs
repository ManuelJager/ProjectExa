using Exa.Bindings;
using Exa.Grids.Ships;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay
{
    public class ShipSelection : ObservableCollection<Ship>
    {
        public bool CanControl { get; protected set; }

        private Dictionary<Ship, UnityAction> callbackDict = new Dictionary<Ship, UnityAction>();

        public virtual void MoveTo(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public override void Add(Ship item)
        {
            base.Add(item);

            item.overlay.overlayCircle.IsSelected = true;

            // Set a callback that removes the ship from the collection when destroyed
            UnityAction callback = () => Remove(item);
            callbackDict.Add(item, callback);
            item.destroyEvent.AddListener(callback);
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
    }
}