using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI.Actions
{
    public class AMoveToTarget : GridAiAction<EnemyGridInstance>
    {
        public override ActionLane Lanes => ActionLane.Movement;

        public ITarget Target { get; set; } = null;

        internal AMoveToTarget(EnemyGridInstance gridInstance)
            : base(gridInstance) { }

        public override ActionLane Update(ActionLane blockedLanes) {
            if (Target == null) return ActionLane.None;

            gridInstance.Navigation.MoveTo = Target;
            return ActionLane.Movement;
        }

        protected override float CalculatePriority() {
            return Target != null
                ? 10
                : 0;
        }
    }
}