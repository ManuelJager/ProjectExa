using Exa.Grids.Blocks.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public abstract class GenericController<T> : Controller, IBehaviourMarker<T>
        where T : struct, IControllerData
    {
        [SerializeField] private BlockBehaviour<T> controllerBehaviour;

        BlockBehaviour<T> IBehaviourMarker<T>.Component => controllerBehaviour;

        public override IEnumerable<BlockBehaviourBase> GetBehaviours() {
            return base.GetBehaviours()
                .Append(controllerBehaviour);
        }
    }
}