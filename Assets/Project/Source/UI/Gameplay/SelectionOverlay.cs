using Exa.Bindings;
using Exa.Gameplay;
using Exa.Grids;
using Exa.Utils;
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
                newView.SetHull(ship.Hull);
                shipViews.Add(key, newView);
            }

            var view = shipViews[key];
            view.Count++;
        }

        public override void OnRemove(Ship data)
        {
            var key = data.Blueprint.name;
            var view = shipViews[key];

            view.Count--;

            if (view.Count == 0)
            {
                shipViews.Remove(key);
                Destroy(view.gameObject);
            }
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