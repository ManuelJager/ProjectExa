﻿using Exa.Bindings;
using Exa.Generics;
using Exa.Ships;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Gameplay
{
    public abstract class ShipSelection : ObservableCollection<Ship>, ICloneable<ShipSelection>
    {
        public bool CanControl { get; protected set; }

        private Dictionary<Ship, UnityAction> callbackDict = new Dictionary<Ship, UnityAction>();

        public override void Add(Ship ship)
        {
            base.Add(ship);

            ship.overlay.overlayCircle.IsSelected = true;

            // Set a callback that removes the ship from the collection when destroyed
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