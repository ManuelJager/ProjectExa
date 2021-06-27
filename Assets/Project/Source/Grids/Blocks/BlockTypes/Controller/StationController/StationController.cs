using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes {
    public abstract class StationController<T> : GenericController<StationControllerData>, IBehaviourMarker<T>
        where T : struct, ITurretValues {
        [SerializeField] protected TurretBehaviour<T> turretBehaviour;

        BlockBehaviour<T> IBehaviourMarker<T>.Component {
            get => turretBehaviour;
        }

        public override IEnumerable<BlockBehaviour> GetBehaviours() {
            return base.GetBehaviours()
                .Append(turretBehaviour);
        }
    }
}