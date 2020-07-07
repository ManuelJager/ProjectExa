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
            this.components = null;
            this.factory = factory;
            IsDirty = true;
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