using Exa.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exa.AI
{
    public class ActionList
    {
        private readonly List<IAction> actions;
        private readonly float priorityThreshold;

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

                if (shouldRun)
                {
                    action.Blocking = action.Update(mask);
                    mask |= action.Blocking;
                }
                else
                {
                    action.Blocking = ActionLane.None;
                }
            }
        }

        private IEnumerable<IAction> SortActions()
        {
            return actions
                .OrderByDescending(action => 
                {
                    action.UpdatePriority();
                    return action.Priority;
                })
                .Where(action => action.Priority > priorityThreshold);
        }

        public string ToString(int tabs = 0)
        {
            var sb = new StringBuilder();
            var tableString = actions
                .OrderByDescending(action => action.Priority)
                .ToStringTable(new[] {
                    "Action name",
                    "Priority",
                    "Blocking lane",
                    "Debug string"},
                    action => action.GetType().Name,
                    action => action.Priority,
                    action => action.Blocking,
                    action => action.DebugString);
            sb.AppendLineIndented(tableString, tabs);
            return sb.ToString();
        }
    }
}