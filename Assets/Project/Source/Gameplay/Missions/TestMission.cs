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
        private Spawner spawner;
        
        public override void Init(MissionArgs args) {
            spawner = new Spawner();
            spawner.SpawnPlayerStation(null, GridInstanceConfiguration.InvulnerableConfig);
            Spawn().Start();
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(1f);
            spawner.SpawnEnemy("defaultScout", 10, 0);
        }
    }
}