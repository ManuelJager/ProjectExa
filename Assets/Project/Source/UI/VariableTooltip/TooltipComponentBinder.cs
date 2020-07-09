using System;

namespace Exa.UI.Tooltips
{
    public class TooltipComponentBinder
    {
        private Action<object> update = null;

        public TooltipComponentBinder() 
        { 
        }

        public TooltipComponentBinder(Action<object> update)
        {
            this.update = update;
        }

        public void Update(object value)
        {
            update?.Invoke(value);
        }
    }
}