using System;
using System.Collections;
using System.Collections.Generic;
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
        private int preparationPhaseLength = 5;
        private WaveState currentWaveState;

        public void Setup(Spawner spawner, List<Wave> waves) {
            this.spawner = spawner;
            this.waves = waves;
        }
        
        public void NextWave() {
            if (currentWaveIndex >= waves.Count) {
                throw new InvalidOperationException("Cannot find wave");
            }
            
            GameSystems.UI.gameplayLayer.missionState.SetText("Combat phase", $"Wave {currentWaveIndex + 1}");

            currentWaveState = new WaveState(OnWaveEnded, OnEnemyDestroyed);
            waves[currentWaveIndex].Spawn(spawner, currentWaveState);

            currentWaveIndex++;
        }

        private void OnEnemyDestroyed(EnemyGrid grid) {
            GameSystems.MissionManager.AddResources(grid.GetBaseTotals().Metadata.blockCosts);
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

        public void StartPreparationPhase(bool firstWave = false) {
            PreparationPhase(firstWave).Start(this);
        }

        private IEnumerator PreparationPhase(bool firstWave) {
            void UpdateText(float time, bool animate) {
                var phaseInfo = "{0} Second/s remaining".Format(Mathf.CeilToInt(time));
                GameSystems.UI.gameplayLayer.missionState.SetText("Preparation phase", phaseInfo, animate);
            }
            
            UpdateText(preparationPhaseLength, !firstWave);

            yield return null;
            
            GameSystems.UI.gameplayLayer.missionState.ShowEditorButton();

            var enumerator = EnumeratorUtils.OnceEverySecond(preparationPhaseLength,
                second =>  UpdateText(preparationPhaseLength - second, true),
                () => !GameSystems.IsPaused);

            while (enumerator.MoveNext(out var current)) {
                yield return current;
            }

            GameSystems.UI.gameplayLayer.missionState.HideEditorButton();
            NextWave();
        }
    }
}