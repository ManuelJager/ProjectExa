using UnityEngine;

namespace Exa.AI.Actions
{
    public class AMoveToTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Movement;
        public override float Priority => Target != null
            ? 10
            : 0;

        public Vector2? Target { get; set; } = null;

        public AMoveToTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target == null) return ActionLane.None;

            shipAI.ship.navigation.SetMoveTo(Target);
            return ActionLane.Movement;
        }
    }
}