using Exa.Grids.Blocks;
using Exa.Ships;
using Exa.Ships.Targetting;
using System.Linq;
using UnityEngine;

namespace Exa.AI.Actions
{
    // TODO: Implement a distance difference threshold that prevents already targeted ships from being untargeted too quickly
    public class AAimAtClosestTarget : ShipAiAction
    {
        public override ActionLane Lanes => ActionLane.AimTurrets;

        private Ship _enemyTarget = null;
        private readonly float _detectionRadius;

        internal AAimAtClosestTarget(Ship ship, float detectionRadius)
            : base(ship)
        {
            this._detectionRadius = detectionRadius;
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            var target = new ShipTarget(_enemyTarget);
            ship.Turrets.SetTarget(target);

            return ActionLane.AimTurrets;
        }

        protected override float CalculatePriority()
        {
            var blockMask = new ShipMask(~ship.BlockContext);
            var closestDistance = float.MaxValue;

            foreach (var enemy in ship.QueryNeighbours<Ship>(_detectionRadius, blockMask))
            {
                var distance = (enemy.transform.position - ship.transform.position).magnitude;

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    _enemyTarget = enemy;
                }
            }

            DebugString = $"Target: {ship.GetInstanceString()}"; 

            if (closestDistance == float.MaxValue)
            {
                _enemyTarget = null;
            }

            return _enemyTarget == null
                ? 0f
                : 5f;
        }
    }
}