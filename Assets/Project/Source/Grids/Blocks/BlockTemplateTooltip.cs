using Exa.UI.Tooltips;
using Exa.Utils;

namespace Exa.Grids.Blocks
{
    public class BlockTemplateTooltip : TooltipView
    {
        public void Show(ShipContext shipContext, string id)
        {
            gameObject.SetActive(true);
            var tooltip = Systems.Blocks.valuesStore.GetTooltip(shipContext, id);
            Systems.UI.tooltips.tooltipGenerator.CreateRootView(tooltip, container);
            SetContainerPosition();
        }

        public void Hide()
        {
            container.DestroyChildren();
            gameObject.SetActive(false);
        }
    }
}