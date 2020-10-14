using Exa.Generics;
using UnityEngine;

namespace Exa.Gameplay.Missions
{
    [CreateAssetMenu(menuName = "Missions/MissionBag")]
    public class MissionBag : ScriptableObjectBag<Mission>
    {
    }
}