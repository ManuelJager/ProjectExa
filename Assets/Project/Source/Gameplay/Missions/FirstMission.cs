using System.Collections.Generic;
using System.Linq;
using Exa.Grids;
using Exa.Grids.Blocks;
using Exa.Grids.Blocks.Components;
using Exa.Grids.Blueprints;
using Exa.Math;
using Exa.Research;
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

            Systems.Instance.Delay(() => {
                var totals = Systems.TotalsManager.StartWatching(Station.Blueprint.Blocks, BlockContext.UserGroup);

                var removeModifier = Systems.Research.AddDynamicModifier(
                    BlockContext.UserGroup,
                    (PhysicalData initial, ref PhysicalData current) => {
                        current.hull *= 10;
                    }
                );

                Debug.Log(totals.Hull);

                removeModifier();

                Debug.Log(totals.Hull);
            }, 1f);

            return;
            
            waveManager = GameSystems.GameObject.AddComponent<WaveManager>();
            waveManager.Setup(spawner, waves);
            waveManager.StartPreparationPhase(true);
        }
    }
}