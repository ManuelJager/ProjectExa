using System;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio
{
    [CreateAssetMenu(menuName = "Audio/SoundBag")]
    [Serializable]
    public class SoundBag : ScriptableObjectBag<Sound>
    { }
}