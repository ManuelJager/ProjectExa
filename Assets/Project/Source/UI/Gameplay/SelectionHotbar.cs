using Exa.Gameplay;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.UI.Gameplay
{
    public class SelectionHotbar : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private GameObject _selectionHotbarItemPrefab;
        private Dictionary<int, SelectionHotbarItem> _itemDict;
        private int _selectedIndex;

        public SelectionHotbarItem CurrentSelection
        {
            get => _itemDict.ContainsKey(_selectedIndex) 
                    ? _itemDict[_selectedIndex] 
                    : null;
        }

        public bool HasSelected
        {
            get => IsInRange(_selectedIndex);
        }

        private void Awake()
        {
            _itemDict = new Dictionary<int, SelectionHotbarItem>();
            _selectedIndex = -1;

            CreatePrefabs();
        }

        public ShipSelection Select(int index = -1)
        {
            if (IsInRange(_selectedIndex))
            {
                var item = _itemDict[_selectedIndex];
                item.Selected = false;
            }

            // If the selected index is the same as the current, treat it as it should be deselected
            if (_selectedIndex == index)
            {
                _selectedIndex = -1;
                return null;
            }

            _selectedIndex = index;

            if (IsInRange(_selectedIndex))
            {
                var selection = _itemDict[index];
                selection.Selected = true;
                return Clone(selection.ShipSelection);
            }

            return null;
        }

        public void Save(ShipSelection selection)
        {
            if (_selectedIndex >= 0 && _selectedIndex <= 9)
            {
                var item = _itemDict[_selectedIndex];
                item.ShipSelection = Clone(selection);
            }
        }

        private void CreatePrefabs()
        {
            for (int i = 1; i < 10; i++)
            {
                CreatePrefab(i);
            }
            CreatePrefab(0);
        }

        private void CreatePrefab(int index)
        {
            var itemGo = Instantiate(_selectionHotbarItemPrefab, _container);
            var item = itemGo.GetComponent<SelectionHotbarItem>();
            item.Setup(index);
            _itemDict[index] = item;
        }

        private ShipSelection Clone(ShipSelection selection)
        {
            return selection != null
                ? selection.Clone() as ShipSelection
                : null;
        }

        private bool IsInRange(int index)
        {
            return index >= 0 && index <= 9;
        }
    }
}