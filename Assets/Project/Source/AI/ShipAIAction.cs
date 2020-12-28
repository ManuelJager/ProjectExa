using Exa.Ships;

namespace Exa.AI
{
    public abstract class GridAiAction<T> : GridAiAction
        where T : GridInstance
    {
        protected T gridInstance;

        internal GridAiAction(T gridInstance) {
            this.gridInstance = gridInstance;
        }
    }

    public abstract class GridAiAction : IAction
    {
        public abstract ActionLane Lanes { get; }
        public ActionLane Blocking { get; set; }
        public float Priority { get; private set; }
        public string DebugString { get; protected set; }


        public abstract ActionLane Update(ActionLane blockedLanes);

        public void UpdatePriority() {
            Priority = CalculatePriority();
        }

        protected abstract float CalculatePriority();
    }
}