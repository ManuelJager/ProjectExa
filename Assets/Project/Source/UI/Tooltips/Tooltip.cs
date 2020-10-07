using System;
using UnityEngine;

namespace Exa.UI.Tooltips
{
    // TODO: Support value refreshing, without destroying and instantiating ui objects
    public class Tooltip
    {
        private readonly Func<TooltipGroup> _factory;
        private TooltipGroup _group;

        public bool ShouldRefresh { get; set; }
        public Font Font { get; private set; }

        public Tooltip(Func<TooltipGroup> factory, Font font = null)
        {
            this._factory = factory;
            ShouldRefresh = true;
            Font = font;
        }

        public TooltipGroup GetRootData()
        {
            if (ShouldRefresh)
            {
                _group = _factory();
            }
            return _group;
        }
    }
}