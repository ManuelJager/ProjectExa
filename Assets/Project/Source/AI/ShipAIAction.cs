namespace Exa.AI
{
    public abstract class ShipAIAction : IAction
    {
        protected ShipAI shipAI;

        public abstract ActionLane Lanes { get; }
        public ActionLane Blocking { get; set; }
        public float Priority { get; private set; }
        public string DebugString { get; protected set; }

        public ShipAIAction(ShipAI shipAI)
        {
            this.shipAI = shipAI;
        }

        public abstract ActionLane Update(ActionLane blockedLanes);

        public void UpdatePriority()
        {
            Priority = CalculatePriority();
        }

        protected abstract float CalculatePriority();
    }
}