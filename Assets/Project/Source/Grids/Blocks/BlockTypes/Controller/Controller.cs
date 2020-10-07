using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Controller : Block, IBehaviourMarker<ControllerData>
    {
        [SerializeField] private ControllerBehaviour _controllerBehaviour;

        BlockBehaviour<ControllerData> IBehaviourMarker<ControllerData>.Component => _controllerBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(_controllerBehaviour);
        }

        protected override void OnAdd()
        {
            Ship.Controller = this;
        }

        protected override void OnRemove()
        {
            Ship.Controller = null;
        }
    }
}