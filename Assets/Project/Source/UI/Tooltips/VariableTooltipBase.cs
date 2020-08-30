using Exa.Utils;

namespace Exa.UI.Tooltips
{
    public abstract class VariableTooltipBase<T> : TooltipBase
        where T : ITooltipPresenter
    {
        private T currentPresenter;
        private Tooltip tooltip;

        public void Show(T presenter)
        {
            gameObject.SetActive(true);
            this.currentPresenter = presenter;
            SetValues(presenter);
            SetContainerPosition();
        }

        protected override void Update()
        {
            if (tooltip != null && tooltip.IsDirty)
            {
                SetValues(currentPresenter);
            }

            base.Update();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetValues(T data)
        {
            container.DestroyChildren();

            tooltip = Systems.UI.tooltips.tooltipGenerator.GenerateTooltip(data, container);
        }
    }
}