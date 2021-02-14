using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission
    {
        [SerializeField] private List<StaticBlueprint> enemyBlueprints;
        private WaveManager waveManager;
        
        public override void Init(MissionArgs args) {
            var spawner = new Spawner(enemyBlueprints.Select(x => x.GetBlueprint()).ToList());
            spawner.SpawnPlayerStation();
            waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner);
            waveManager.NextWave();
        }
    }
}