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
        [SerializeField] private List<Wave> waves;
        
        private WaveManager waveManager;
        
        public override void Init(MissionArgs args) {
            var spawner = new Spawner();
            Station = spawner.SpawnPlayerStation();
            
            waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);
        }
    }
}