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
            SpawnMothership(args.fleet.mothership.Data);
            StartCoroutine(Spawn());
            
        }

        private IEnumerator Spawn() {
            yield return new WaitForSeconds(0.5f);
            SpawnFriendly("defaultScout", 20, 20, new GridInstanceConfiguration {
                Invulnerable = true
            });

            yield return new WaitForSeconds(0.5f);
            SpawnEnemy("defaultScout", 30, 20, new GridInstanceConfiguration {
                Invulnerable = true
            });
        }
    }
}