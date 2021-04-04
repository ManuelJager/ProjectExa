using System;
using Exa.Ships;

namespace Exa.Gameplay.Missions
{
    public class WaveState
    {
        private readonly Action onWaveEnded;
        
        private int totalCount;
        private int totalDestroyed;
        private bool finishedSpawning;

        public WaveState(Action onWaveEnded) {
            this.onWaveEnded = onWaveEnded;
        }
        
        public void OnEnemySpawned(EnemyGrid grid) {
            totalCount++;
            grid.ControllerDestroyedEvent.AddListener(() => OnEnemyDestroyed(grid));
        }

        public void OnFinishSpawning() {
            finishedSpawning = true;
        }
        
        private void OnEnemyDestroyed(EnemyGrid grid) {
            totalDestroyed++;
            
            // Add on the costs of the destroyed ship to the current resources
            GameSystems.MissionManager.CurrentResources += grid.GetBaseTotals().Metadata.blockCosts;

            if (totalDestroyed == totalCount && finishedSpawning) {
                onWaveEnded();
            }
        }
    }
}