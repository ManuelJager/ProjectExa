using System.Collections.Generic;
using System.Linq;

namespace Exa.UI.Tooltips
{
    public class TooltipBinder<T>
        where T : ITooltipPresenter
    {
        private List<TooltipComponentBinder> tooltipComponentBinders;

        internal TooltipBinder(List<TooltipComponentBinder> tooltipComponentBinders)
        {
            this.tooltipComponentBinders = tooltipComponentBinders;
        }

        public void Update(T value)
        {
            var result = value.GetTooltip()
                .GetComponents()
                .ToList();

            for (int i = 0; i < result.Count; i++)
            {
                var component = result[i];
                tooltipComponentBinders[i].Update(component);
            }
        }
    }
}