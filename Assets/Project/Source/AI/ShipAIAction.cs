using Exa.Ships;

namespace Exa.AI
{
    public abstract class ShipAIAction : IAction
    {
        protected Ship ship;

        public abstract ActionLane Lanes { get; }
        public ActionLane Blocking { get; set; }
        public float Priority { get; private set; }
        public string DebugString { get; protected set; }

        internal ShipAIAction(Ship ship)
        {
            this.ship = ship;
        }

        public abstract ActionLane Update(ActionLane blockedLanes);

        public void UpdatePriority()
        {
            Priority = CalculatePriority();
        }

        protected abstract float CalculatePriority();
    }
}