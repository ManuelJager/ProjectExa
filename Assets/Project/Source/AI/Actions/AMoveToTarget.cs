using UnityEngine;

namespace Exa.AI.Actions
{
    public class AMoveToTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Movement;
        public override float Priority => 10;

        public AMoveToTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            return ActionLane.None;
        }
    }
}