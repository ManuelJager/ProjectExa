using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blueprints;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class WaveManager : MonoBehaviour
    {
        private Spawner spawner;
        private int wave;

        public void Setup(Spawner spawner) {
            this.spawner = spawner;
        }
        
        public void NextWave() {
            wave++;
            
            GameSystems.UI.gameplayLayer.missionState.SetText("Combat phase", $"Wave {wave}");
            
            var enemies = spawner.SpawnFormationAtRandomPosition(new VicFormation(), 100f).ToList();
            var totalCount = enemies.Count;
            var totalDestroyed = 0;
            
            foreach (var ship in enemies) {
                ship.ControllerDestroyedEvent.AddListener(() => {
                    totalDestroyed++;
                    if (totalDestroyed == totalCount) {
                        NextWave();
                    }
                });
            }
        }
    }
}