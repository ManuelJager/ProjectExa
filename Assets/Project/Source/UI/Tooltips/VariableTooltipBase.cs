using Exa.Utils;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public abstract class VariableTooltipBase<T> : TooltipBase
        where T : ITooltipPresenter
    {
        //private TooltipBinder<T> binder;

        public void ShowTooltip(T data)
        {
            gameObject.SetActive(true);
            SetValues(data);
            SetContainerPosition();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        public virtual void SetValues(T data)
        {
            container.DestroyChildren();

            Systems.UI.tooltips.tooltipGenerator.GenerateTooltip(data, container);
        }
    }
}