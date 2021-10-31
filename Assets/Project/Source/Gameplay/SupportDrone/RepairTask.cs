using Exa.Grids;
using Exa.Grids.Blocks.BlockTypes;
using UnityEngine;

namespace Exa.Gameplay {
    public class RepairTask : SupportDroneTask {
        public Block target;

        public RepairTask(Block target)
            : base(SupportDroneTaskType.Repair) {
            this.target = target;
        }

        public override Vector2 GetPosition() {
            return target.GetGlobalPosition(target.GridInstance);
        }
    }
}