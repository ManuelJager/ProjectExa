using UnityEngine;

namespace Exa.AI
{
    public class ALookAtTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Rotation;

        public Vector2? Target { get; set; } = null;

        public ALookAtTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target == null) return ActionLane.None;

            shipAI.ship.navigation.SetLookAt(Target);
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