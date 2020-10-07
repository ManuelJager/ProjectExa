using Exa.UI.Tooltips;
using Exa.Utils;

namespace Exa.Grids.Blocks
{
    public class BlockTemplateTooltip : TooltipView
    {
        public void Show(ShipContext shipContext, BlockTemplate blockTemplate)
        {
            gameObject.SetActive(true);
            var tooltip = Systems.Blocks.valuesStore.GetTooltip(shipContext, blockTemplate);
            Systems.Ui.tooltips.tooltipGenerator.CreateRootView(tooltip, container);
            SetContainerPosition();
        }

        public void Hide()
        {
            container.DestroyChildren();
            gameObject.SetActive(false);
        }
    }
}