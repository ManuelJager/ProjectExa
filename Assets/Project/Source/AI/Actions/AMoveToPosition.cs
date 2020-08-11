using UnityEngine;

namespace Exa.AI.Actions
{
    public class AMoveToPosition : Action
    {
        public override ActionLane Lanes => ActionLane.Movement;
        public override float Priority => 10;

        public override void Run(ref ActionLane blockedLanes)
        {
        }
    }
}