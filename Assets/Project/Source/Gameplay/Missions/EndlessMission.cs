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
            SpawnPlayerStation(GridInstanceConfiguration.InvulnerableConfig);
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy("defaultScout", 10, 0);
        }
    }
}