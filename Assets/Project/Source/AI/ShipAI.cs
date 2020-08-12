using Exa.AI.Actions;
using Exa.Ships;
using UnityEngine;

namespace Exa.AI
{
    public class ShipAI : Agent
    {
        public Ship ship;

        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public AAimAtTarget aimAtTarget;
        public ALookAtTarget lookAtTarget;
        public AMoveToTarget moveToTarget;

        private void Awake()
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
                aimAtTarget = new AAimAtTarget(this),
                lookAtTarget = new ALookAtTarget(this),
                moveToTarget = new AMoveToTarget(this)
            });
        }

        public string ToString(int tabs = 0)
        {
            return actionList.ToString(tabs);
        }
    }
}