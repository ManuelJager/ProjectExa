namespace Exa.AI
{
    public abstract class ShipAIAction : IAction
    {
        protected ShipAI shipAI;

        public bool IsRunning { get; set; }
        public abstract ActionLane Lanes { get; }
        public ActionLane Blocking { get; set; }
        public abstract float Priority { get; }

        public ShipAIAction(ShipAI shipAI)
        {
            this.shipAI = shipAI;
        }

        public abstract ActionLane Update(ActionLane blockedLanes);
    }
}