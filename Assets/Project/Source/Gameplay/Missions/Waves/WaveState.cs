using System;
using Exa.Ships;

namespace Exa.Gameplay.Missions {
    public class WaveState {
        private readonly Action onWaveEnded;
        private readonly Action<EnemyGrid> onEnemySpawned;
        private readonly Action<EnemyGrid> onEnemyDestroyed;
        private bool finishedSpawning;

        private int totalCount;
        private int totalDestroyed;

        public WaveState(Action onWaveEnded, Action<EnemyGrid> onEnemySpawned, Action<EnemyGrid> onEnemyDestroyed) {
            this.onWaveEnded = onWaveEnded;
            this.onEnemySpawned = onEnemySpawned;
            this.onEnemyDestroyed = onEnemyDestroyed;
        }

        public void OnEnemySpawned(EnemyGrid grid) {
            totalCount++;

            grid.ControllerDestroyed += () => OnEnemyDestroyed(grid);
            onEnemySpawned(grid);
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