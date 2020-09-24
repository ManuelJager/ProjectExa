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
    public class ShipAi : Agent
    {
        public AAimAtClosestTarget aimAtTarget;
        public ALookAtTarget lookAtTarget;
        public AMoveToTarget moveToTarget;
        public AAvoidCollision avoidCollision;

        [SerializeField] private Ship ship;
        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public Ship Ship => ship;

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
                aimAtTarget = new AAimAtClosestTarget(Ship, 200f),
                lookAtTarget = new ALookAtTarget(Ship),
                moveToTarget = new AMoveToTarget(Ship),
                avoidCollision = new AAvoidCollision(Ship, new AAvoidCollisionSettings
                {
                    detectionRadius = Ship.BlockGrid.MaxSize,
                    priorityMultiplier = 1,
                    priorityBase = 10,
                    headingCorrectionMultiplier = 8
                })
            });
        }

        public IEnumerable<ITooltipComponent> GetDebugTooltipComponents() => new ITooltipComponent[]
        {
            new TooltipText(actionList.ToString())
        };
    }
}