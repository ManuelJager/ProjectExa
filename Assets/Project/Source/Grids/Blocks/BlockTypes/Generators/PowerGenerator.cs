using Exa.Grids.Blocks.Components;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class PowerGenerator : PhysicalBlock
    {
        [SerializeField] private PowerGeneratorBehaviour powerGeneratorBehaviour;

        public PowerGeneratorBehaviour PowerGeneratorBehaviour { get => powerGeneratorBehaviour; set => powerGeneratorBehaviour = value; }

        public override Ship Ship
        {
            set
            {
                base.Ship = value;
                powerGeneratorBehaviour.Ship = value;
            }
        }
    }
}