using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI
{
    public class ALookAtTarget : ShipAiAction
    {
        public override ActionLane Lanes => ActionLane.Rotation;

        public ITarget Target { get; set; } = null;

        internal ALookAtTarget(GridInstance gridInstance)
            : base(gridInstance) { }

        public override ActionLane Update(ActionLane blockedLanes) {
            if (Target == null) return ActionLane.None;

            gridInstance.Navigation.LookAt = Target;
            return ActionLane.Rotation;
        }

        protected override float CalculatePriority() {
            return Target != null
                ? 2
                : 0;
        }
    }
}