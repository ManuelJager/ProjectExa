using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Exa.Ships;

#pragma warning disable CS0649

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : Block, IThruster
    {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;

        BlockBehaviour<ThrusterData> IBehaviourMarker<ThrusterData>.Component => thrusterBehaviour;

        public void Fire(float strength) {
            thrusterBehaviour.Fire(strength);
        }

        public void PowerDown() {
            thrusterBehaviour.PowerDown();
        }

        public override IEnumerable<BlockBehaviourBase> GetBehaviours() {
            return base.GetBehaviours()
                .Append(thrusterBehaviour);
        }

        protected override void OnAdd() {
            (GridInstance as EnemyGridInstance)?.Navigation.ThrustVectors.Register(this);
        }

        protected override void OnRemove() {
            (GridInstance as EnemyGridInstance)?.Navigation.ThrustVectors.Unregister(this);
        }
    }
}