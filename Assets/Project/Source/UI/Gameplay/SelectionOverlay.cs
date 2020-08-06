using Exa.Bindings;
using Exa.Gameplay;
using Exa.Grids;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public partial class SelectionOverlay : AbstractCollectionObserver<Ship>
    {
        [SerializeField] private GameObject shipViewPrefab;

        private Dictionary<string, ShipView> shipViews = new Dictionary<string, ShipView>();

        public override void OnAdd(Ship data)
        {
            var key = data.Blueprint.name;

            if (!shipViews.ContainsKey(key))
            {
                var newView = Instantiate(shipViewPrefab).GetComponent<ShipView>();
                shipViews.Add(key, newView);
            }

            var view = shipViews[key];
            view.Count++;
        }

        public override void OnRemove(Ship data)
        {
            var view = shipViews[data.Blueprint.name];

            view.Count--;

            if (view.Count == 0)
            {
                Destroy(view.gameObject);
            }
        }

        public void Reflect(ShipSelection shipSelection)
        {
            this.Source = shipSelection;
        }
    }
}