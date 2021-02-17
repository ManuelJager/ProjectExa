using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [Serializable]
    public class Wave : IWave
    {
        [SerializeField] private List<StaticBlueprint> blueprints;

        public IEnumerable<Blueprint> Blueprints => blueprints.Select(b => b.GetBlueprint());
        
        // TODO: Implement some kind of mechanism that distributes blueprints based on the blueprint and wave strength
        public IEnumerable<Blueprint> GetSpawnAbleBlueprints() {
            return Enumerable.Repeat(Blueprints.GetRandomElement(), 5);
        }

        public void Spawn(Spawner spawner, WaveState state) {
            foreach (var enemy in spawner.SpawnFormationAtRandomPosition(this, new VicFormation(), 100f)) {
                state.OnEnemySpawned(enemy);
            }

            state.OnFinishSpawning();
        }
    }
}