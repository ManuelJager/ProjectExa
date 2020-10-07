using Exa.Ships;
using Exa.Ships.Targetting;

namespace Exa.AI
{
    public class ALookAtTarget : ShipAiAction
    {
        public override ActionLane Lanes => ActionLane.Rotation;

        public ITarget Target { get; set; } = null;

        internal ALookAtTarget(Ship ship)
            : base(ship)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target == null) return ActionLane.None;

            ship.Navigation.LookAt = Target;
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