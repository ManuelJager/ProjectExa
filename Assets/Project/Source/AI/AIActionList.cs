using Exa.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Exa.AI
{
    public class AIActionList
    {
        private List<IAIAction> actions;
        private float priorityThreshold;

        public AIActionList(float priorityThreshold, IAIAction[] actions)
        {
            this.actions = new List<IAIAction>(actions);
            this.priorityThreshold = priorityThreshold;
        }

        public void Add(IAIAction action)
        {
            actions.Add(action);
        }

        public void RunActions()
        {
            AIActionLane mask = 0;

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

        private IEnumerable<IAIAction> SortActions()
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