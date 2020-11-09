using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI.Actions
{
    // TODO: Implement a distance difference threshold that prevents already targeted ships from being untargeted too quickly
    public class AAimAtClosestTarget : ShipAiAction
    {
        public override ActionLane Lanes => ActionLane.AimTurrets;

        private Ship enemyTarget = null;
        private readonly float detectionRadius;

        internal AAimAtClosestTarget(Ship ship, float detectionRadius)
            : base(ship) {
            this.detectionRadius = detectionRadius;
        }

        public override ActionLane Update(ActionLane blockedLanes) {
            var target = new ShipTarget(enemyTarget);
            ship.Turrets.SetTarget(target);

            return ActionLane.AimTurrets;
        }

        protected override float CalculatePriority() {
            var blockMask = new ShipMask(~ship.BlockContext);
            var closestDistance = float.MaxValue;

            foreach (var enemy in ship.QueryNeighbours(detectionRadius, blockMask)) {
                var distance = (enemy.transform.position - ship.transform.position).magnitude;

                if (distance < closestDistance) {
                    closestDistance = distance;
                    enemyTarget = enemy;
                }
            }

            DebugString = $"Target: {ship.GetInstanceString()}";

            if (closestDistance == float.MaxValue) {
                enemyTarget = null;
            }

            return enemyTarget == null
                ? 0f
                : 5f;
        }
    }
}