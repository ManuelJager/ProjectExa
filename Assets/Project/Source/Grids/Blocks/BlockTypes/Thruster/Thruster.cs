using Exa.Grids.Blocks.Components;
using UnityEngine;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : PhysicalBlock
    {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;

        public ThrusterBehaviour ThrusterBehaviour
        {
            get => thrusterBehaviour;
            set => thrusterBehaviour = value;
        }
    }
}