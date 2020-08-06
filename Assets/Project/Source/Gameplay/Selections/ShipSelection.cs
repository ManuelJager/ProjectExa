using Exa.Bindings;
using Exa.Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            // Set a callback that removes the ship from the collection when destroyed
            UnityAction callback = () => Remove(item);
            callbackDict.Add(item, callback);
            item.destroyEvent.AddListener(callback);
        }

        public override bool Remove(Ship item)
        {
            // Get the callback and remove it
            var callback = callbackDict[item];
            callbackDict.Remove(item);
            item.destroyEvent.RemoveListener(callback);

            return base.Remove(item);
        }
    }
}
