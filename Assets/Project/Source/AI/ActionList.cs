using Exa.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Exa.AI
{
    public class ActionList
    {
        private List<IAction> actions;
        private float priorityThreshold;

        public ActionList(float priorityThreshold, IAction[] actions)
        {
            this.actions = new List<IAction>(actions);
            this.priorityThreshold = priorityThreshold;
        }

        public void Add(IAction action)
        {
            actions.Add(action);
        }

        public void RunActions()
        {
            ActionLane mask = 0;

            foreach (var action in SortActions())
            {
                var shouldRun = (mask & action.Lanes) == 0;

                action.IsRunning = shouldRun;

                if (shouldRun && !action.IsRunning)
                {
                    mask |= action.Lanes;
                    action.Run(ref mask);
                }
            }
        }

        private IEnumerable<IAction> SortActions()
        {
            return actions
                .Where((action) => action.Priority > priorityThreshold)
                .OrderBy((action) => action.Priority);
        }

        public override string ToString()
        {
            return actions.ToStringTable(
                (action) => action.GetType().Name,
                (action) => action.Priority,
                (action) => action.IsRunning);
        }
    }
}