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
        
        private int currentWaveIndex;
        private float preparationPhaseLength = 5f;
        private WaveState currentWaveState;
        
        public bool PausedPreparationPhase { get; set; }

        public void Setup(Spawner spawner, List<Wave> waves) {
            this.spawner = spawner;
            this.waves = waves;
        }
        
        public void NextWave() {
            if (currentWaveIndex >= waves.Count) {
                throw new InvalidOperationException("Cannot find wave");
            }
            
            GameSystems.UI.gameplayLayer.missionState.SetText("Combat phase", $"Wave {currentWaveIndex + 1}");

            currentWaveState = new WaveState(OnWaveEnded);
            waves[currentWaveIndex].Spawn(spawner, currentWaveState);

            currentWaveIndex++;
        }

        private void OnMissionEnded() {
            Debug.Log("Mission ended");
        }

        private void OnWaveEnded() {
            Debug.Log("Wave ended");

            if (currentWaveIndex >= waves.Count) {
                OnMissionEnded();
            }
            else {
                StartPreparationPhase();
            }
        }

        public void StartPreparationPhase() {
            PreparationPhase().Start(this);
        }

        private IEnumerator PreparationPhase() {
            void UpdateText(float time) {
                var phaseInfo = "{0} Second/s remaining".Format(Mathf.CeilToInt(time));
                GameSystems.UI.gameplayLayer.missionState.SetText("Preparation phase", phaseInfo);
            }
            
            var timeRemaining = preparationPhaseLength;
            UpdateText(timeRemaining);

            yield return null;
            
            GameSystems.UI.gameplayLayer.missionState.ShowEditorButton();
            
            while (timeRemaining > 0f) {
                if (!PausedPreparationPhase) {
                    timeRemaining -= Time.deltaTime;
                    UpdateText(timeRemaining);
                }
                
                yield return null;
            }
            
            GameSystems.UI.gameplayLayer.missionState.HideEditorButton();
            NextWave();
        }
    }
}