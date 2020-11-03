using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/Endless")]
    public class EndlessMission : Mission
    {
        public override void Init(MissionArgs args) {
            SpawnMothership(args.fleet.mothership.Data);
            SpawnFriendly("defaultScout", 20, 20);
        }
    }
}