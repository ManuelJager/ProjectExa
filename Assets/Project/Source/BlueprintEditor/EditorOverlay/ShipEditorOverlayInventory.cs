﻿using System;
using Exa.Grids.Blocks;
using Exa.UI.Components;
using System.Collections.Generic;
using System.Linq;
using Exa.Types.Binding;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

#pragma warning disable CS0649

namespace Exa.ShipEditor
{
    public class BlockSelectedEvent : UnityEvent<BlockTemplate>
    { }

    public class ShipEditorOverlayInventory : ViewBinder<BlockTemplateView, BlockTemplateContainer, BlockTemplate>
    {
        public BlockSelectedEvent blockSelected = new BlockSelectedEvent();

        [SerializeField] private GameObject expandableItemPrefab;

        private readonly Dictionary<BlockCategory, ExpandableItem> blockCategories =
            new Dictionary<BlockCategory, ExpandableItem>();

        private BlockTemplateView activeView;
        private BlockCategory filter = BlockCategory.All;

        public void Init() {
            Source = Systems.Blocks.blockTemplates;
        }

        public void SelectFirst() {
            var selectedBlock = Source.First(value => filter.HasValue(value.Data.category));
            SetSelected(selectedBlock);
            blockCategories[selectedBlock.Data.category].Expanded = true;
        }

        public void SetFilter(BlockCategory blockCategoryFilter) {
            filter = blockCategoryFilter;
            foreach (var (category, item) in blockCategories.Unpack()) {
                item.gameObject.SetActive(blockCategoryFilter.HasValue(category));
            }
        }

        public override void OnAdd(BlockTemplateContainer value) {
            var category = value.Data.category;
            var categoryString = category.ToFriendlyString();

            if (!blockCategories.ContainsKey(category)) {
                var newExpandableItemObject = Instantiate(expandableItemPrefab, viewContainer);
                var newExpandableItem = newExpandableItemObject.GetComponent<ExpandableItem>();
                newExpandableItem.HeaderText = categoryString;
                blockCategories[category] = newExpandableItem;
                newExpandableItem.gameObject.SetActive(filter.HasValue(category));
            }

            var categoryItem = blockCategories[category];
            var view = base.CreateView(value, categoryItem.content);

            view.button.onClick.AddListener(() => SetSelected(value));
        }

        public void SetSelected(BlockTemplateContainer value) {
            if (activeView != null)
                activeView.Selected = false;

            activeView = GetView(value);
            activeView.Selected = true;

            blockSelected?.Invoke(value.Data);
        }
    }
}