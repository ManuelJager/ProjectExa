using Exa.Generics;
using System;
using UnityEngine;

namespace Exa.Audio
{
    [CreateAssetMenu(menuName = "Audio/SoundBag")]
    [Serializable]
    public class SoundBag : ScriptableObjectBag<Sound>
    {
    }
}