using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : Block, IThruster
    {
        [SerializeField] private ThrusterBehaviour _thrusterBehaviour;

        BlockBehaviour<ThrusterData> IBehaviourMarker<ThrusterData>.Component => _thrusterBehaviour;

        public void Fire(float strength)
        {
            _thrusterBehaviour.Fire(strength);
        }

        public override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(_thrusterBehaviour);
        }

        protected override void OnAdd()
        {
            Ship.Navigation.ThrustVectors.Register(this);
        }

        protected override void OnRemove()
        {
            Ship.Navigation.ThrustVectors.Unregister(this);
        }
    }
}