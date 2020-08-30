using System;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    public class Tooltip
    {
        private Func<TooltipContainer> factory;
        private TooltipContainer container;
        public bool IsDirty { get; set; }
        public Font Font { get; private set; }

        public Tooltip(Func<TooltipContainer> factory, Font font = null)
        {
            this.factory = factory;
            IsDirty = true;
            Font = font;
        }

        public TooltipContainer GetContainer()
        {
            if (IsDirty)
            {
                container = factory();
            }
            return container;
        }
    }
}