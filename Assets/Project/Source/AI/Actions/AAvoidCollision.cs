using Exa.Math;
using Exa.Ships;
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

    public class AAvoidCollision : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Movement;

        private List<Ship> neighbourCache;
        private AAvoidCollisionSettings settings;

        public AAvoidCollision(ShipAI shipAI, AAvoidCollisionSettings settings) : base(shipAI)
        {
            this.settings = settings;
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            var globalPos = shipAI.transform.position.ToVector2();
            var currentVel = shipAI.ship.navigation.rb.velocity.ToVector2();
            var headingVector = currentVel.normalized;

            foreach (var neighbour in neighbourCache)
            {
                var neighbourPos = neighbour.transform.position.ToVector2();

                MofidyHeading(ref headingVector, neighbourPos - globalPos, neighbour);
            }

            headingVector = headingVector.normalized;
            var target = globalPos + Vector2.ClampMagnitude(headingVector * settings.headingCorrectionMultiplier, settings.detectionRadius);

            shipAI.ship.navigation.SetMoveTo(target);

            return ActionLane.Movement;
        }

        protected override float CalculatePriority()
        {
            var globalPos = shipAI.transform.position;
            var mask = LayerMask.GetMask("unit");
            var colliders = Physics2D.OverlapCircleAll(globalPos, settings.detectionRadius, mask);
            var shortestDistance = float.MaxValue;

            neighbourCache = new List<Ship>();
            foreach (var collider in colliders)
            {
                var neighbour = collider.gameObject.GetComponent<Ship>();
                if (neighbour != null && !ReferenceEquals(neighbour, shipAI.ship))
                {
                    var neighbourPos = neighbour.transform.position;
                    var dist = (globalPos - neighbourPos).magnitude;

                    if (shortestDistance > dist)
                    {
                        shortestDistance = dist;
                    }

                    neighbourCache.Add(neighbour);
                }
            }

            if (shortestDistance == float.MaxValue)
            {
                return 0f;
            }

            // Calculate the priority value due to distance to the shortest 
            var distancePriority = (settings.detectionRadius - shortestDistance) / settings.detectionRadius * settings.priorityMultiplier;

            return distancePriority + settings.priorityBase;
        }

        private void MofidyHeading(ref Vector2 heading, Vector2 direction, Ship other)
        {
            var thisMass = shipAI.ship.Blueprint.Blocks.Totals.Mass;
            var otherMass = other.Blueprint.Blocks.Totals.Mass;
            var headingModification = direction * (otherMass / thisMass) / settings.detectionRadius;
            heading -= headingModification;
        }
    }
}