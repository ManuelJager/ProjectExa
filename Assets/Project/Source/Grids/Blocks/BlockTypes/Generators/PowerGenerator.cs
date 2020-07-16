using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : PhysicalBlock
    {
        [SerializeField] private PowerGeneratorBehaviour powerGeneratorBehaviour;

        public PowerGeneratorBehaviour PowerGeneratorBehaviour { get => powerGeneratorBehaviour; set => powerGeneratorBehaviour = value; }
    }
}