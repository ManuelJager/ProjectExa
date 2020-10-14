using Exa.Ships;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    public abstract class Mission : ScriptableObject
    {
        public string missionName;

        public abstract void Init(MissionArgs args);
    }
}