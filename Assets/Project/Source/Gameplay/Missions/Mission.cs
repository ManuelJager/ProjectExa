using Exa.Grids.Blueprints;
using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public abstract class Mission : ScriptableObject
    {
        public string missionName;
        public string missionDescription;

        public abstract void Init(MissionArgs args);

        protected void SpawnMothership(BlueprintContainer mothership)
        {
            GameSystems.ShipFactory.CreateFriendly(mothership.Data, new Vector2(-20, 20));
        }
    }
}