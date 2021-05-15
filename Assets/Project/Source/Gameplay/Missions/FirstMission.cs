using System.Collections.Generic;
using Exa.Grids.Blocks;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission
    {
        [SerializeField] private List<Wave> waves;
        
        public override void Init(MissionArgs args) {
            var spawner = new Spawner();

            GameSystems.MissionManager.AddResources(new BlockCosts {
                creditCost = 100,
                metalsCost = 10,
            });
            
            GameSystems.MissionManager.Station = spawner.SpawnPlayerStation();
            
            var waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);

            waveManager.WaveStarted += () => {
                GameSystems.MissionManager.Station.Repair();
                GameSystems.MissionManager.Station.ReconcileWithDiff();
            };
        }
    }
}