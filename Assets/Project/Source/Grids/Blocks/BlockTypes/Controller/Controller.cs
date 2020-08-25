using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Controller : Block, IController
    {
        [SerializeField] private ControllerBehaviour controllerBehaviour;

        public BlockBehaviour<ControllerData> Component
        {
            get => controllerBehaviour;
            set => controllerBehaviour = value as ControllerBehaviour;
        }

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