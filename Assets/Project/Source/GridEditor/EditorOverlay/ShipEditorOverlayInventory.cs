using System;
using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI.Components;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class BlockSelectedEvent : UnityEvent<BlockTemplate>
    { }

    public class ShipEditorOverlayInventory : ViewController<BlockTemplateView, BlockTemplateContainer, BlockTemplate>
    {
        public BlockSelectedEvent blockSelected = new BlockSelectedEvent();

        [SerializeField] private GameObject expandableItemPrefab;

        private readonly Dictionary<BlockCategory, ExpandableItem> blockCategories =
            new Dictionary<BlockCategory, ExpandableItem>();

        private BlockTemplateView activeView;
        private BlockCategory filter;

        private void Start() {
            Source = Systems.Blocks.availableBlockTemplates;
        }

        public void SetFilter(BlockCategory blockCategoryFilter) {
            filter = blockCategoryFilter;
            foreach (var (category, item) in blockCategories.Unpack()) {
                item.gameObject.SetActive(category.Is(blockCategoryFilter));
            }
        }

        public override void OnAdd(BlockTemplateContainer value) {
            var category = value.Data.category;
            if (!filter.Is(category)) {
                return;
            }

            var categoryString = value.Data.category.ToString();

            if (!blockCategories.ContainsKey(category)) {
                var newExpandableItemObject = Instantiate(expandableItemPrefab, viewContainer);
                var newExpandableItem = newExpandableItemObject.GetComponent<ExpandableItem>();
                newExpandableItem.HeaderText = categoryString;
                blockCategories[category] = newExpandableItem;
            }

            var categoryItem = blockCategories[category];
            var view = base.OnAdd(value, categoryItem.content);

            view.button.onClick.AddListener(() => {
                if (activeView != null)
                    activeView.Selected = false;

                activeView = view;
                activeView.Selected = true;

                blockSelected?.Invoke(value.Data);
            });
        }
    }
}