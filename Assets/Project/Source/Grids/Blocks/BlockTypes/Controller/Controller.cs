using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Controller : Block, IBehaviourMarker<ControllerData>
    {
        [SerializeField] private ControllerBehaviour controllerBehaviour;

        BlockBehaviour<ControllerData> IBehaviourMarker<ControllerData>.Component => controllerBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(controllerBehaviour);
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