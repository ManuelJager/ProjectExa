using Exa.Grids.Blocks;
using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targeting;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.AI.Actions
{
    public class AAvoidCollisionSettings
    {
        public float detectionRadius;
        public float priorityMultiplier;
        public float priorityBase;
        public float headingCorrectionMultiplier;
    }

    // TODO: Use position prediction and target paths to increase accuracy
    public class AAvoidCollision : GridAiAction<EnemyGridInstance>
    {
        public override ActionLane Lanes => ActionLane.Movement;

        private List<GridInstance> neighbourCache;
        private readonly AAvoidCollisionSettings settings;

        internal AAvoidCollision(EnemyGridInstance gridInstance, AAvoidCollisionSettings settings)
            : base(gridInstance) {
            this.settings = settings;
        }

        public override ActionLane Update(ActionLane blockedLanes) {
            var globalPos = gridInstance.transform.position.ToVector2();
            var currentVel = gridInstance.Rigidbody2D.velocity;
            var headingVector = currentVel.normalized;

            foreach (var neighbour in neighbourCache) {
                var neighbourPos = neighbour.transform.position.ToVector2();

                MofidyHeading(ref headingVector, neighbourPos - globalPos, neighbour);
            }

            headingVector = headingVector.normalized;
            var offset = Vector2.ClampMagnitude(headingVector * settings.headingCorrectionMultiplier,
                settings.detectionRadius);
            var target = new StaticPositionTarget(globalPos + offset);

            gridInstance.Navigation.MoveTo = target;

            return ActionLane.Movement;
        }

        // TODO: Improve detection of large ships, as this only registers other ships whose centre overlaps the detection radius
        protected override float CalculatePriority() {
            var globalPos = gridInstance.transform.position;
            var shipMask = new ShipMask(~BlockContext.None);
            var shortestDistance = float.MaxValue;

            neighbourCache?.Clear();
            neighbourCache = neighbourCache ?? new List<GridInstance>();

            foreach (var neighbour in gridInstance.QueryNeighbours(settings.detectionRadius, shipMask)) {
                if (!ShouldYield(neighbour)) continue;

                var neighbourPos = neighbour.transform.position;
                var dist = (globalPos - neighbourPos).magnitude;

                if (shortestDistance > dist) {
                    shortestDistance = dist;
                }

                neighbourCache.Add(neighbour);
            }

            if (shortestDistance == float.MaxValue) {
                return 0f;
            }

            // Calculate the priority value due to distance to the shortest 
            var distancePriority = (settings.detectionRadius - shortestDistance) / settings.detectionRadius *
                                   settings.priorityMultiplier;

            return distancePriority + settings.priorityBase;
        }

        private void MofidyHeading(ref Vector2 heading, Vector2 direction, GridInstance other) {
            if (!ShouldYield(other)) return;

            var headingModification = direction / settings.detectionRadius;
            heading -= headingModification;
        }

        private bool ShouldYield(GridInstance other) {
            var thisMass = gridInstance.Blueprint.Blocks.Totals.Mass;
            var otherMass = other.Blueprint.Blocks.Totals.Mass;

            if (otherMass != thisMass) {
                return otherMass > thisMass;
            }

            return other.GetInstanceID() > gridInstance.GetInstanceID();
        }
    }
}