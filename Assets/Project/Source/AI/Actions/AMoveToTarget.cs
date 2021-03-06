﻿using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI.Actions
{
    public class AMoveToTarget : ShipAiAction
    {
        public override ActionLane Lanes => ActionLane.Movement;

        public ITarget Target { get; set; } = null;

        internal AMoveToTarget(Ship ship)
            : base(ship) { }

        public override ActionLane Update(ActionLane blockedLanes) {
            if (Target == null) return ActionLane.None;

            ship.Navigation.MoveTo = Target;
            return ActionLane.Movement;
        }

        protected override float CalculatePriority() {
            return Target != null
                ? 10
                : 0;
        }
    }
}