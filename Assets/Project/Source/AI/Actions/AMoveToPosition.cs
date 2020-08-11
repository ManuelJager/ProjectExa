using UnityEngine;

namespace Exa.AI.Actions
{
    public class AMoveToPosition : AIAction
    {
        public override AIActionLane Lanes => AIActionLane.Movement;
        public override float Priority => 10;

        public override void Run(ref AIActionLane blockedLanes)
        {
        }
    }
}