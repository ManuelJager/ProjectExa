using Exa.AI.Actions;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.AI
{
    public class ShipAI : Agent
    {
        [SerializeField] private Ship ship;
        [SerializeField] private float activeValueThreshold;
        private ActionList actionList;

        public AAimAtTarget aAimAtTarget;
        public AMoveToPosition AMoveToTarget;

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
                aAimAtTarget = new AAimAtTarget(),
                AMoveToTarget = new AMoveToPosition()
            });
        }

        public override string ToString()
        {
            return 
                $"{GetType().Name}\n" +
                actionList.ToString();
        }
    }
}