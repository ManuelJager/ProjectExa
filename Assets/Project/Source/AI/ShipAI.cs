using Exa.AI.Actions;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.AI
{
    public class ShipAI : MonoBehaviour
    {
        [SerializeField] private Ship ship;
        [SerializeField] private float activeValueThreshold;
        private AIActionList actionList;

        public AAimAtTarget aAimAtTarget;
        public AMoveToPosition AMoveToTarget;

        private void Awake()
        {
            actionList = BuildActionList();
            actionList.RunActions();
        }

        protected virtual AIActionList BuildActionList()
        {
            return new AIActionList(activeValueThreshold, new IAIAction[]
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