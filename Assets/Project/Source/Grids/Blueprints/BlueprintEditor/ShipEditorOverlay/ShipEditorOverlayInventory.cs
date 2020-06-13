using Exa.Bindings;
using Exa.Grids.Blocks;
using Exa.UI.Controls;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Grids.Blueprints.BlueprintEditor
{
    public class ShipEditorOverlayInventory : ViewController<BlockTemplateView, ObservableBlockTemplate, BlockTemplate>
    {
        [SerializeField] private GameObject expandableItemPrefab;
        private Dictionary<string, ExpandableItem> blockCategories = new Dictionary<string, ExpandableItem>();

        public event BlockSelectedDelegate BlockSelected;

        public override void OnAdd(ObservableBlockTemplate observer)
        {
            var category = observer.Data.Category;
            var displayCategory = observer.Data.DisplayCategory;

            if (!blockCategories.ContainsKey(category))
            {
                var newExpandableItemObject = Instantiate(expandableItemPrefab, viewContainer);
                var newExpandableItem = newExpandableItemObject.GetComponent<ExpandableItem>();
                newExpandableItem.HeaderText = displayCategory;
                blockCategories[category] = newExpandableItem;
            }

            var categoryItem = blockCategories[category];
            base.OnAdd(observer, categoryItem.content);
        }

        public override void ViewCreation(BlockTemplateView view, ObservableBlockTemplate observer)
        {
            view.button.onClick.AddListener(() =>
            {
                BlockSelected?.Invoke(observer.Data);
            });
        }
    }
}