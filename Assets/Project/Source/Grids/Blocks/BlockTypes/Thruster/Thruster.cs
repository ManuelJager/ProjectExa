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

        BlockBehaviour<ThrusterData> IBehaviourMarker<ThrusterData>.Component
        {
            get => thrusterBehaviour;
            set => thrusterBehaviour = value as ThrusterBehaviour;
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(thrusterBehaviour);
        }

        protected override void OnAdd()
        {
            Ship.blockGrid.ThrustVectors.Register(this);
        }

        protected override void OnRemove()
        {
            Ship.blockGrid.ThrustVectors.Unregister(this);
        }
    }
}