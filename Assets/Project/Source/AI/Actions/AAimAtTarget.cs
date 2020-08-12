﻿using UnityEngine;

namespace Exa.AI.Actions
{
    public class AAimAtTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Target;
        public override float Priority => 10;

        public AAimAtTarget(ShipAI shipAI)
            : base(shipAI)
        {
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            return ActionLane.None;
        }
    }
}