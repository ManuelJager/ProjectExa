using Exa.AI.Actions;
using Exa.Grids.Blocks;
using Exa.Ships;
using Exa.UI.Tooltips;
using Exa.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.AI
{
    public class ShipAI : Agent
    {
        public Ship ship;

        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public AAimAtClosestTarget aimAtTarget;
        public ALookAtTarget lookAtTarget;
        public AMoveToTarget moveToTarget;
        public AAvoidCollision avoidCollision;

        public void Initialize()
        {
            actionList = BuildActionList();
        }

        public override void AIUpdate()
        {
            actionList.RunActions();
        }

        protected virtual ActionList BuildActionList()
        {
            return new ActionList(activeValueThreshold, new IAction[]
            {
                aimAtTarget = new AAimAtClosestTarget(this, 200f),
                lookAtTarget = new ALookAtTarget(this),
                moveToTarget = new AMoveToTarget(this),
                avoidCollision = new AAvoidCollision(this, new AAvoidCollisionSettings
                {
                    detectionRadius = ship.blockGrid.MaxSize,
                    priorityMultiplier = 1,
                    priorityBase = 10,
                    headingCorrectionMultiplier = 8
                })
            });
        }

        // TODO: Somehow cache this, or let the results come from a central manager
        public IEnumerable<T> QueryNeighbours<T>(float radius, ShipMask shipMask)
            where T : Ship
        {
            var colliders = Physics2D.OverlapCircleAll(ship.transform.position, radius, shipMask.LayerMask);

            foreach (var collider in colliders)
            {
                var neighbour = collider.gameObject.GetComponent<T>();
                var passesContextMask = (neighbour.BlockContext & shipMask.ContextMask) != 0;
                if (neighbour != null && !ReferenceEquals(neighbour, ship) && passesContextMask)
                {
                    yield return neighbour;
                }
            }
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText(actionList.ToString())
        };
    }
}