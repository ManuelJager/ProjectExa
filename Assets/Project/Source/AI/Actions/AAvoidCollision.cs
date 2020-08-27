using Exa.Math;
using Exa.Ships;
using Exa.Ships.Targetting;
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
    public class AAvoidCollision : ShipAIAction
    {
        public override ActionLane Lanes => ActionLane.Movement;

        private List<Ship> neighbourCache;
        private AAvoidCollisionSettings settings;

        public AAvoidCollision(ShipAI shipAI, AAvoidCollisionSettings settings) 
            : base(shipAI)
        {
            this.settings = settings;
        }

        public override ActionLane Update(ActionLane blockedLanes)
        {
            var globalPos = shipAI.transform.position.ToVector2();
            var currentVel = shipAI.ship.rigidbody.velocity.ToVector2();
            var headingVector = currentVel.normalized;

            foreach (var neighbour in neighbourCache)
            {
                var neighbourPos = neighbour.transform.position.ToVector2();

                MofidyHeading(ref headingVector, neighbourPos - globalPos, neighbour);
            }

            headingVector = headingVector.normalized;
            var offset = Vector2.ClampMagnitude(headingVector * settings.headingCorrectionMultiplier, settings.detectionRadius);
            var target = new StaticPositionTarget(globalPos + offset);

            shipAI.ship.navigation.SetMoveTo(target);

            return ActionLane.Movement;
        }

        // TODO: Improve detection of large ships, as this only registers other ships whose centre overlaps the detection radius
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
                if (neighbour != null && !ReferenceEquals(neighbour, shipAI.ship) && ShouldYield(neighbour))
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
            if (ShouldYield(other))
            {
                var headingModification = direction / settings.detectionRadius;
                heading -= headingModification;
            }
        }

        private bool ShouldYield(Ship other)
        {
            var thisMass = shipAI.ship.Blueprint.Blocks.Totals.Mass;
            var otherMass = other.Blueprint.Blocks.Totals.Mass;

            if (otherMass != thisMass)
            {
                return otherMass > thisMass;
            }

            return other.GetInstanceID() > shipAI.ship.GetInstanceID();
        }
    }
}