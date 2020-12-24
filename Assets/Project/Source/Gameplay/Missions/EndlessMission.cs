using System.Collections;
using Exa.Grids;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/Endless")]
    public class EndlessMission : Mission
    {
        public override void Init(MissionArgs args) {
            SpawnPlayerStation(new GridInstanceConfiguration {
                Invulnerable = true
            });
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy("defaultScout", 30, 20, new GridInstanceConfiguration {
                Invulnerable = true
            });
        }
    }
}