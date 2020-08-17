using UnityEngine;

namespace Exa.AI.Actions
{
    public class AAimAtTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Target;

        public AAimAtTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            return ActionLane.None;
        }

        protected override float CalculatePriority()
        {
            return 10f;
        }
    }
}