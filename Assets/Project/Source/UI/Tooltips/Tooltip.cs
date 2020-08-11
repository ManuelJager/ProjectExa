using System;
using System.Collections.Generic;

namespace Exa.UI.Tooltips
{
    public class Tooltip
    {
        private Func<IEnumerable<ITooltipComponent>> factory;
        private IEnumerable<ITooltipComponent> components;
        public bool IsDirty { get; set; }

        public Tooltip(Func<IEnumerable<ITooltipComponent>> factory)
        {
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