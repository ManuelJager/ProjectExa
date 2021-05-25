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
        private Action<EnemyGrid> onEnemyDestroyed;

        public WaveState(Action onWaveEnded, Action<EnemyGrid> onEnemyDestroyed) {
            this.onWaveEnded = onWaveEnded;
            this.onEnemyDestroyed = onEnemyDestroyed;
        }
        
    public void OnEnemySpawned(EnemyGrid grid) {
            totalCount++;
            
            grid.ControllerDestroyed += () => OnEnemyDestroyed(grid);
        }

        public void OnFinishSpawning() {
            finishedSpawning = true;
        }
        
        private void OnEnemyDestroyed(EnemyGrid grid) {
            totalDestroyed++;
            
            // Add on the costs of the destroyed ship to the current resources
            onEnemyDestroyed?.Invoke(grid);
            
            if (totalDestroyed == totalCount && finishedSpawning) {
                onWaveEnded();
            }
        }
    }
}