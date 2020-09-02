using Exa.Grids.Blocks;
using Exa.Ships;
using Exa.Ships.Targetting;
using System.Linq;
using UnityEngine;

namespace Exa.AI.Actions
{
    // TODO: Implement a distance difference threshold that prevents already targeted ships from being untargeted too quickly
    public class AAimAtClosestTarget : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.AimTurrets;

        private Ship enemyTarget = null;
        private float detectionRadius;

        internal AAimAtClosestTarget(ShipAI shipAI, float detectionRadius)
            : base(shipAI)
        {
            this.detectionRadius = detectionRadius;
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            var target = new ShipTarget(enemyTarget);
            shipAI.ship.Turrets.SetTarget(target);

            return ActionLane.AimTurrets;
        }

        protected override float CalculatePriority()
        {
            var blockMask = new ShipMask(~shipAI.ship.BlockContext);
            var closestDistance = float.MaxValue;

            foreach (var enemy in shipAI.QueryNeighbours<Ship>(detectionRadius, blockMask))
            {
                var distance = (enemy.transform.position - shipAI.transform.position).magnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    enemyTarget = enemy;
                }
            }

            DebugString = $"Target: {shipAI.ship.GetInstanceString()}"; 

            if (closestDistance == float.MaxValue)
            {
                enemyTarget = null;
            }

            return enemyTarget == null
                ? 0f
                : 5f;
        }
    }
}