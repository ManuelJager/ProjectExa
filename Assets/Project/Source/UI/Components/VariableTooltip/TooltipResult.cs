using System;
using System.Collections.Generic;

namespace Exa.UI.Tooltips
{
    public class TooltipResult
    {
        private Func<IEnumerable<ITooltipComponent>> factory;
        private IEnumerable<ITooltipComponent> components;
        public bool IsDirty { get; set; }

        public TooltipResult(Func<IEnumerable<ITooltipComponent>> factory)
        {
            this.components = factory();
            this.factory = factory;
            IsDirty = false;
        }

        public IEnumerable<ITooltipComponent> GetComponents()
        {
            if (IsDirty)
            {
                components = factory();
                IsDirty = false;
            }
            return components;
        }
    }
}