using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/Endless")]
    public class EndlessMission : Mission
    {
        public override void Init(MissionArgs args)
        {
            GameSystems.ShipFactory.CreateFriendly("defaultScout", new Vector2(-20, 20));
        }
    }
}