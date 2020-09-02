using Exa.UI.Tooltips;

namespace Exa.Ships
{
    public class ShipDebugTooltip : TooltipView<Ship>
    {
        protected override void Update()
        {
            Root?.Refresh(tooltip.GetRootData());

            base.Update();
        }
    }
}