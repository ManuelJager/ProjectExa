using Exa.Ships;
using Exa.Ships.Targeting;

namespace Exa.AI.Actions
{
    // TODO: Implement a distance difference threshold that prevents already targeted ships from being untargeted too quickly
    public class AAimAtClosestTarget : GridAiAction<GridInstance>
    {
        public override ActionLane Lanes => ActionLane.AimTurrets;

        private GridInstance enemyTarget = null;
        private readonly float detectionRadius;

        internal AAimAtClosestTarget(GridInstance gridInstance, float detectionRadius)
            : base(gridInstance) {
            this.detectionRadius = detectionRadius;
        }

        public override ActionLane Update(ActionLane blockedLanes) {
            var target = new ShipTarget(enemyTarget);
            grid.BlockGrid.Metadata.TurretList.SetTarget(target);

            return ActionLane.AimTurrets;
        }

        protected override float CalculatePriority() {
            var blockMask = (~grid.BlockContext).GetShipMask();
            var closestDistance = float.MaxValue;

            foreach (var enemy in grid.QueryNeighbours(detectionRadius, blockMask, true)) {
                var distance = (enemy.transform.position - grid.transform.position).magnitude;

                if (distance < closestDistance) {
                    closestDistance = distance;
                    enemyTarget = enemy;
                }
            }

            if (closestDistance == float.MaxValue) {
                enemyTarget = null;
            }

            DebugString = $"Target: {enemyTarget?.GetInstanceString()}";

            return enemyTarget == null
                ? 0f
                : 5f;
        }
    }
}