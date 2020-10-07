using Exa.Bindings;
using Exa.Gameplay;
using Exa.Ships;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public partial class SelectionOverlay : AbstractCollectionObserver<Ship>
    {
        [SerializeField] private GameObject _shipViewPrefab;
        [SerializeField] private Transform _container;

        private readonly Dictionary<string, ShipView> _shipViews = new Dictionary<string, ShipView>();

        private void Awake()
        {
            _container.gameObject.SetActive(false);
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
            var view = _shipViews[key];

            // Destroy the view if there are ships it could represent
            // A 1 is used here instead of a 0 as we assume the Ship will be removed from the view
            if (view.Count == 1)
            {
                _shipViews.Remove(key);
                Destroy(view.gameObject);
            }

            view.Remove(ship);
            ProcessEnabled();
        }

        public override void OnClear()
        {
            foreach (var view in _shipViews.Values)
            {
                Destroy(view.gameObject);
            }

            _shipViews.Clear();
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
            if (!_shipViews.ContainsKey(key))
            {
                var viewGo = Instantiate(_shipViewPrefab, _container);
                var view = viewGo.GetComponent<ShipView>();
                view.SetThumbnail(ship.Blueprint.Thumbnail);
                _shipViews.Add(key, view);
                return view;
            }
            else
            {
                return _shipViews[key];
            }
        }

        private void ProcessEnabled()
        {
            var active = Source != null && Source.Count() > 0;
            _container.gameObject.SetActive(active);
        }
    }
}