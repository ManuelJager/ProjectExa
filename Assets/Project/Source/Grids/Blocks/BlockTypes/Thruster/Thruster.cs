using Exa.Grids.Blocks.Components;
using Exa.Grids.Ships;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : PhysicalBlock, IThruster
    {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;

        public ThrusterBehaviour ThrusterBehaviour
        {
            get => thrusterBehaviour;
            set => thrusterBehaviour = value;
        }

        public override Ship Ship
        {
            set
            {
                base.Ship = value;
                thrusterBehaviour.Ship = value;
            }
        }
    }
}