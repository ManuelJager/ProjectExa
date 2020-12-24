using Exa.Ships;

namespace Exa.AI
{
    public abstract class ShipAiAction : IAction
    {
        protected GridInstance gridInstance;

        public abstract ActionLane Lanes { get; }
        public ActionLane Blocking { get; set; }
        public float Priority { get; private set; }
        public string DebugString { get; protected set; }

        internal ShipAiAction(GridInstance gridInstance) {
            this.gridInstance = gridInstance;
        }

        public abstract ActionLane Update(ActionLane blockedLanes);

        public void UpdatePriority() {
            Priority = CalculatePriority();
        }

        protected abstract float CalculatePriority();
    }
}