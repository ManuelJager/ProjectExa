using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : Block, IThruster
    {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;

        public ThrusterBehaviour ThrusterBehaviour
        {
            get => thrusterBehaviour;
            set => thrusterBehaviour = value;
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(thrusterBehaviour);
        }
    }
}