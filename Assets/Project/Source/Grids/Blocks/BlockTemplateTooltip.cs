using Exa.UI.Tooltips;
using Exa.Utils;

namespace Exa.Grids.Blocks
{
    public class BlockTemplateTooltip : FloatingTooltip
    {
        public void Show(ShipContext shipContext, BlockTemplate blockTemplate)
        {
            gameObject.SetActive(true);
            var tooltip = Systems.Blocks.valuesStore.GetTooltip(shipContext, blockTemplate);
            Systems.UI.tooltips.tooltipGenerator.CreateRootView(tooltip, itemsContainer);
            UpdatePosition(true);
        }

        public void Hide()
        {
            itemsContainer.DestroyChildren();
            gameObject.SetActive(false);
        }
    }
}