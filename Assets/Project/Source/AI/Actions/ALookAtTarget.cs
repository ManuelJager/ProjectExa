using Exa.Ships.Targetting;

namespace Exa.AI
{
    public class ALookAtTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Rotation;

        public ITarget Target { get; set; } = null;

        internal ALookAtTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target == null) return ActionLane.None;

            shipAI.ship.navigation.LookAt = Target;
            return ActionLane.Rotation;
        }

        protected override float CalculatePriority()
        {
            return Target != null
                ? 2
                : 0;
        }
    }
}