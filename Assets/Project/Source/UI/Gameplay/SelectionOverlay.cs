using Exa.Bindings;
using Exa.Gameplay;
using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.UI.Gameplay
{
    public class SelectionOverlay : AbstractCollectionObserver<Ship>
    {
        [SerializeField] private GameObject shipViewPrefab;
        [SerializeField] private Transform container;

        private readonly Dictionary<string, ShipView> shipViews = new Dictionary<string, ShipView>();

        private void Awake()
        {
            container.gameObject.SetActive(false);
        }

        public override void OnAdd(Ship ship)
        {
            var view = SelectOrCreateView(ship);
            view.Add(ship);
            ProcessEnabled();
        }

        public override void OnRemove(Ship ship)
        {
            var key = ship.Blueprint.name;
            var view = shipViews[key];

            // Destroy the view if there are ships it could represent
            // A 1 is used here instead of a 0 as we assume the Ship will be removed from the view
            if (view.Count == 1)
            {
                shipViews.Remove(key);
                Destroy(view.gameObject);
            }

            view.Remove(ship);
            ProcessEnabled();
        }

        public override void OnClear()
        {
            foreach (var view in shipViews.Values)
            {
                Destroy(view.gameObject);
            }

            shipViews.Clear();
            ProcessEnabled();
        }

        public void Reflect(ShipSelection shipSelection)
        {
            this.Source = shipSelection;
            ProcessEnabled();
        }

        private ShipView SelectOrCreateView(Ship ship)
        {
            var key = ship.Blueprint.name;
            if (!shipViews.ContainsKey(key))
            {
                var viewGO = Instantiate(shipViewPrefab, container);
                var view = viewGO.GetComponent<ShipView>();
                view.SetThumbnail(ship.Blueprint.Thumbnail);
                shipViews.Add(key, view);
                return view;
            }
            else
            {
                return shipViews[key];
            }
        }

        private void ProcessEnabled()
        {
            var active = Source != null && Source.Count() > 0;
            container.gameObject.SetActive(active);
        }
    }
}