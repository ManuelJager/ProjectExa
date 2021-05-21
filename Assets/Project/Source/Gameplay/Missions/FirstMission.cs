using System.Collections.Generic;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Research;
using NaughtyAttributes;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission
    {
        [SerializeField] private BlockCosts initialResources;
        [SerializeField] private float resourceMultiplier;
        [SerializeField] private List<Wave> waves;
        
        public override void Init(MissionManager manager, MissionArgs args) {
            base.Init(manager, args);
            
            var spawner = new Spawner();

            manager.CurrentResources = initialResources;
            manager.Station = spawner.SpawnPlayerStation();
            
            var waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);

            waveManager.WaveStarted += () => {
                GameSystems.MissionManager.Station.Repair();
                GameSystems.MissionManager.Station.ReconcileWithDiff();
            };

            waveManager.EnemyDestroyed += grid => {
                var costs = grid.GetBaseTotals().Metadata.blockCosts * resourceMultiplier;
                GameSystems.MissionManager.AddResources(costs);
            };
        }

        protected override void AddResearchModifiers(ResearchBuilder builder) => builder
            .Context(BlockContext.UserGroup)
            .Add((ref AutocannonData curr) => curr.damage *= 0.5f)
            .Context(BlockContext.EnemyGroup)
            .Add((ref AutocannonData curr) => curr.damage *= 0.02f);
    }
}