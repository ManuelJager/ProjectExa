﻿using UnityEngine;

namespace Exa.AI
{
    public class ALookAtTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Rotation;

        public override float Priority => Target != null
            ? 2
            : 0;

        public Vector2? Target { get; set; } = null;

        public ALookAtTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            if (Target != null)
            {
                shipAI.ship.navigation.SetLookat(Target);
                return ActionLane.Rotation;
            }

            return ActionLane.None;
        }
    }
}