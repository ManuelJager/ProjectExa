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
            Systems.UI.tooltips.tooltipGenerator.CreateRootView(tooltip, container);
            UpdatePosition(true);
        }

        public void Hide()
        {
            container.DestroyChildren();
            gameObject.SetActive(false);
        }
    }
}