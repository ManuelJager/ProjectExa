using System.Collections;
using Exa.Grids;
using Exa.Ships;
using Exa.Utils;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/Test")]
    public class TestMission : Mission
    {
        public override void Init(MissionArgs args) {
            SpawnPlayerStation(GridInstanceConfiguration.InvulnerableConfig);
            Spawn().Start();
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy("defaultScout", 10, 0);
        }
    }
}