using Exa.Grids.Blocks.Components;
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
    }
}