using Exa.Bindings;
using Exa.Gameplay;
using Exa.Grids.Ships;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public partial class SelectionOverlay : AbstractCollectionObserver<Ship>
    {
        [SerializeField] private GameObject shipViewPrefab;
        [SerializeField] private Transform container;

        private Dictionary<string, ShipView> shipViews = new Dictionary<string, ShipView>();

        public override void OnAdd(Ship ship)
        {
            var key = ship.Blueprint.name;

            if (!shipViews.ContainsKey(key))
            {
                var viewGO = Instantiate(shipViewPrefab, container);
                var newView = viewGO.GetComponent<ShipView>();
                newView.SetThumbnail(ship.Blueprint.Thumbnail);
                shipViews.Add(key, newView);
            }

            var view = shipViews[key];
            view.Add(ship);
        }

        public override void OnRemove(Ship ship)
        {
            var key = ship.Blueprint.name;
            var view = shipViews[key];

            // Destroy the view if there are ships it could represent
            // A 1 is used here instead of a 0 as we assume the ship will be removed from the view
            if (view.Count == 1)
            {
                shipViews.Remove(key);
                Destroy(view.gameObject);
            }

            view.Remove(ship);
        }

        public override void OnClear()
        {
            foreach (var view in shipViews.Values)
            {
                Destroy(view.gameObject);
            }

            shipViews.Clear();
        }

        public void Reflect(ShipSelection shipSelection)
        {
            this.Source = shipSelection;
            container.gameObject.SetActive(shipSelection != null);
        }
    }
}