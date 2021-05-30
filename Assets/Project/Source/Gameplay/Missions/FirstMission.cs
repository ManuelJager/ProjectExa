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

            manager.Station.ControllerDestroyed += () => {
                GS.UI.gameplayLayer.NavigateTo(GS.UI.gameOverMenu);
            };

            manager.Station.Controller.PhysicalBehaviour.OnDamage += damage => {
                GS.UI.gameplayLayer.damageOverlay.NotifyDamage();
            };
            
            var waveManager = GS.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);

            waveManager.WaveStarted += () => {
                GS.MissionManager.Station.Repair();
                GS.MissionManager.Station.ReconcileWithDiff();
            };

            waveManager.EnemyDestroyed += grid => {
                var costs = grid.GetBaseTotals().Metadata.blockCosts * resourceMultiplier;
                GS.MissionManager.AddResources(costs);
            };
        }

        protected override void AddResearchModifiers(ResearchBuilder builder) => builder
            .Context(BlockContext.UserGroup)
            .Add((ref AutocannonData curr) => curr.damage *= 0.5f);
    }
}