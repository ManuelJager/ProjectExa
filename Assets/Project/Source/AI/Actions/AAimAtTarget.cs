using UnityEngine;

namespace Exa.AI.Actions
{
    public class AAimAtTarget : AIAction
    {
        public override AIActionLane Lanes => AIActionLane.Target;
        public override float Priority => 10;

        public override void Run(ref AIActionLane blockedLanes)
        {
        }
    }
}