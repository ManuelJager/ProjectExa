using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI.Components;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.ShipEditor
{
    public class BlockSelectedEvent : UnityEvent<BlockTemplate>
    {
    }

    public class ShipEditorOverlayInventory : ViewController<BlockTemplateView, BlockTemplateContainer, BlockTemplate>
    {
        public BlockSelectedEvent blockSelected = new BlockSelectedEvent();

        [SerializeField] private GameObject expandableItemPrefab;
        private readonly Dictionary<BlockCategory, ExpandableItem> blockCategories = new Dictionary<BlockCategory, ExpandableItem>();

        private void Start()
        {
            Source = Systems.Blocks.availibleBlockTemplates;
        }

        public override void OnAdd(BlockTemplateContainer observer)
        {
            var category = observer.Data.category;
            var categoryString = observer.Data.category.ToString();

            if (!blockCategories.ContainsKey(category))
            {
                var newExpandableItemObject = Instantiate(expandableItemPrefab, viewContainer);
                var newExpandableItem = newExpandableItemObject.GetComponent<ExpandableItem>();
                newExpandableItem.HeaderText = categoryString;
                blockCategories[category] = newExpandableItem;
            }

            var categoryItem = blockCategories[category];
            base.OnAdd(observer, categoryItem.content);
        }

        public override void ViewCreation(BlockTemplateView view, BlockTemplateContainer observer)
        {
            view.button.onClick.AddListener(() =>
            {
                blockSelected?.Invoke(observer.Data);
            });
        }
    }
}