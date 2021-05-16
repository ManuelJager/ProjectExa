using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Research;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission
    {
        [SerializeField] private List<Wave> waves;
        
        public override void Init(MissionArgs args) {
            base.Init(args);
            
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

        protected override void AddResearchModifiers(ResearchBuilder builder) => builder
            .Context(BlockContext.UserGroup)
            .Add((ref AutocannonData curr) => curr.damage *= 0.5f)
            .Context(BlockContext.EnemyGroup)
            .Add((ref AutocannonData curr) => curr.damage *= 0.02f);
    }
}