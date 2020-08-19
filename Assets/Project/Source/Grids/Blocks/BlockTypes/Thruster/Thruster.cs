using Exa.Ships;
using Exa.Grids.Blocks.Components;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Exa.Grids.Blocks.BlockTypes
{
    public class Thruster : Block, IThruster, IPowerConsumer
    {
        [SerializeField] private ThrusterBehaviour thrusterBehaviour;
        [SerializeField] private PowerConsumerBehaviour powerConsumerBehaviour;

        BlockBehaviour<ThrusterData> IBehaviourMarker<ThrusterData>.Component
        {
            get => thrusterBehaviour;
            set => thrusterBehaviour = value as ThrusterBehaviour;
        }

        BlockBehaviour<PowerConsumerData> IBehaviourMarker<PowerConsumerData>.Component 
        { 
            get => powerConsumerBehaviour; 
            set => powerConsumerBehaviour = value as PowerConsumerBehaviour; 
        }

        protected override IEnumerable<BlockBehaviourBase> GetBehaviours()
        {
            return base.GetBehaviours()
                .Append(thrusterBehaviour)
                .Append(powerConsumerBehaviour);
        }

        protected override void OnAdd()
        {
            var archetype = new ThrusterArchetype
            {
                thrusterBehaviour = thrusterBehaviour,
                powerConsumerBehaviour = powerConsumerBehaviour
            };

            Ship.blockGrid.ThrustVectors.Register(gameObject, archetype);
        }

        protected override void OnRemove()
        {
            Ship.blockGrid.ThrustVectors.Unregister(gameObject);
        }
    }
}