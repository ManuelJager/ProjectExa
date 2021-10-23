using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.BlockTypes;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Research;
using Exa.Types.Generics;
using Exa.UI;
using Exa.UI.Controls;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions {
    [CreateAssetMenu(menuName = "Missions/First")]
    public class FirstMission : Mission {
        [SerializeField] private BlockCosts initialResources;
        [SerializeField] private float resourceMultiplier;
        [SerializeField] private List<DynamicStrengthWave> waves;

        private Blueprint selectedBlueprint;

        public override void Init(MissionManager manager) {
            base.Init(manager);

            var spawner = new Spawner();

            manager.CurrentResources = initialResources;
            manager.Station = spawner.SpawnPlayerStation(selectedBlueprint);

            manager.Station.ControllerDestroyed += () => {
                GS.UI.gameOverMenu.scoreView.PresentStats(manager.Stats);
                GS.UI.gameplayLayer.NavigateTo(GS.UI.gameOverMenu);
            };

            manager.Station.Controller.PhysicalBehaviour.OnDamage += damage => { GS.UI.gameplayLayer.damageOverlay.NotifyDamage(); };

            foreach (var wave in waves) {
                wave.Setup(GaugeCurrentStrength);
            }

            var waveManager = GS.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);

            waveManager.WaveStarted += () => {
                GS.MissionManager.Station.Repair();
                GS.MissionManager.Station.ReconcileWithDiff();
            };

            waveManager.EnemySpawned += grid => { GS.UI.gameplayLayer.warningCircleOverlay.Add(grid); };

            waveManager.EnemyDestroyed += grid => {
                var costs = grid.BlueprintTotals().Metadata.blockCosts * resourceMultiplier;
                GS.UI.gameplayLayer.warningCircleOverlay.Remove(grid);
                GS.MissionManager.AddResources(costs);
                GS.MissionManager.Stats.CollectedResources += costs;
                GS.MissionManager.Stats.DestroyedShips += 1;
            };
        }

        public override void BuildStartOptions(MissionOptions options) {
            var dropdown = DropdownControl.Create(
                options.MissionStartOptionContainer,
                label: "Blueprint",
                possibleValues: S.Blueprints.useableBlueprints
                    .Where(container => container.Data.shipClass == BlueprintTypeGuid.station)
                    .Select<BlueprintContainer, ILabeledValue<Blueprint>>(container => new LabeledValue<Blueprint>(container.Data.name, container.Data)),
                setter: selection => selectedBlueprint = selection
            );

            selectedBlueprint = dropdown.Value as Blueprint;
        }

        protected override void AddResearchModifiers(ResearchBuilder builder) {
            builder
                .Context(BlockContext.EnemyGroup)
                .Add((ref AutocannonData curr) => curr.damage *= 0.5f)
                .Context(BlockContext.UserGroup)
                .Add((ref ShieldGeneratorData curr) => curr.health *= 0.1f)
                .Filter<GaussCannonTemplate>()
                .Add((ref PhysicalData curr) => curr.hull *= 10f)
                .Filter(template => !(template is GaussCannonTemplate))
                .Add((ref PhysicalData curr) => {
                    curr.hull *= 0.2f;
                    curr.armor *= 0.5f;
                })
                .ClearFilter();
        }

        private float GaugeCurrentStrength() {
            // Calculate station strength
            var stationStrength = GS.MissionManager.Station.BlueprintTotals().Metadata.strength;

            // Somewhat punish hoarding resources by taking it into account
            var resourcesStrength = GS.MissionManager.CurrentResources.GaugePotentialStrength() * 0.5f;

            return stationStrength + resourcesStrength;
        }
    }
}