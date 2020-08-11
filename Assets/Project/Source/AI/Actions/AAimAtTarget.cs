using UnityEngine;

namespace Exa.AI.Actions
{
    public class AAimAtTarget : Action
    {
        public override ActionLane Lanes => ActionLane.Target;
        public override float Priority => 10;

        public override void Run(ref ActionLane blockedLanes)
        {
        }
    }
}