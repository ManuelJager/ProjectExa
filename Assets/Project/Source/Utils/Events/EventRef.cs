using System;

namespace Exa.Utils
{
    public class EventRef
    {
        private Action removeAction;

        internal EventRef(Action removeAction)
        {
            this.removeAction = removeAction;
        }

        public void RemoveListenerFromTarget()
        {
            removeAction();
        }
    }
}
