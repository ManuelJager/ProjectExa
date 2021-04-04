using System.Collections.Generic;
using Exa.Grids.Blocks;
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

            GameSystems.MissionManager.CurrentResources = new BlockMetadata.BlockCosts {
                creditCost = 100,
                metalsCost = 10,
            };
            
            Station = spawner.SpawnPlayerStation();
            
            waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);
        }
    }
}