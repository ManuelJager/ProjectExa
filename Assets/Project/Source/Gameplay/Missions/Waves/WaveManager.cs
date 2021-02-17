using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exa.Grids.Blueprints;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public class WaveManager : MonoBehaviour
    {
        private Spawner spawner;
        private List<Wave> waves;
        
        private int waveIndex;
        private WaveState currentWaveState;

        public void Setup(Spawner spawner, List<Wave> waves) {
            this.spawner = spawner;
            this.waves = waves;
        }
        
        public void NextWave() {
            if (waveIndex >= waves.Count) {
                throw new InvalidOperationException("Cannot find wave");
            }
            
            GameSystems.UI.gameplayLayer.missionState.SetText("Combat phase", $"Wave {waveIndex + 1}");
            
            waves[waveIndex].Spawn(spawner, currentWaveState = new WaveState(OnWaveEnded));

            waveIndex++;
        }

        private void OnMissionEnded() {
            Debug.Log("Mission ended");
        }

        private void OnWaveEnded() {
            Debug.Log("Wave ended");

            if (waveIndex >= waves.Count) {
                OnMissionEnded();
            }
            else {
                PreparationPhase().Start(this);
            }
        }

        private IEnumerator PreparationPhase() {
            for (var i = 15; i > 0; i--) {
                GameSystems.UI.gameplayLayer.missionState.SetText("Preparation phase", $"{i} Second/s remaining");
                yield return new WaitForSeconds(1);
            }

            NextWave();
        }
    }
}