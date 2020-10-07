using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : Block, IBehaviourMarker<PowerGeneratorData>
    {
        [SerializeField] private PowerGeneratorBehaviour _powerGeneratorBehaviour;

        public BlockBehaviour<PowerGeneratorData> Component => _powerGeneratorBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(_powerGeneratorBehaviour);
        }
    }
}