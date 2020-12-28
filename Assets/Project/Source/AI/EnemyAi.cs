using System.Collections.Generic;
using System.Linq;
using Exa.AI.Actions;
using Exa.Ships;

namespace Exa.AI
{
    public class EnemyAi : GridAi 
    {
        protected override IEnumerable<IAction> BuildActionList() {
            return base.BuildActionList()
                .Append(new ALookAtTarget(GridInstance as EnemyGridInstance))
                .Append(new AMoveToTarget(GridInstance as EnemyGridInstance))
                .Append(new AAvoidCollision(GridInstance as EnemyGridInstance, new AAvoidCollisionSettings {
                    detectionRadius = GridInstance.BlockGrid.MaxSize,
                    priorityMultiplier = 1,
                    priorityBase = 10,
                    headingCorrectionMultiplier = 8
                }));
        }
    }
}