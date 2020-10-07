using Exa.Utils;
using System;

namespace Exa.UI.Tooltips
{
    public abstract class TooltipView<T> : TooltipView
        where T : ITooltipPresenter
    {
        protected Tooltip tooltip;

        public GroupView Root { get; set; }

        public void Show(T presenter)
        {
            gameObject.SetActive(true);
            Rebuild(presenter);
            SetContainerPosition();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Rebuild(T data)
        {
            container.DestroyChildren();
            tooltip = data.GetTooltip();
            Root = Systems.Ui.tooltips.tooltipGenerator.CreateRootView(tooltip, container);
            tooltip.ShouldRefresh = false;
        }
    }
}