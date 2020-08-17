using Exa.Math;
using UnityEngine;

namespace Exa.AI.Actions
{
    public class AMoveToTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Movement;

        public Vector2? Target { get; set; } = null;

        public AMoveToTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target == null) return ActionLane.None;

            var currentPosition = shipAI.transform.position.ToVector2();
            var distanceToTarget = (Target.Value - currentPosition).magnitude;

            // If we are close enough to the target, discard the action
            if (distanceToTarget < 1)
            {
                Target = null;
                return ActionLane.None;
            }

            shipAI.ship.navigation.SetMoveTo(Target);
            return ActionLane.Movement;
        }

        protected override float CalculatePriority()
        {
            return Target != null
                ? 10
                : 0;
        }
    }
}