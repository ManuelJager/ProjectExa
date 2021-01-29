using System.Collections.Generic;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio.Music
{
    [CreateAssetMenu(menuName = "Audio/TrackList")]
    public class DefaultSoundTrack : ScriptableObjectBag<Song>, ISoundTrack
    {
        IEnumerator<ISong> IEnumerable<ISong>.GetEnumerator() {
            return GetEnumerator();
        }
    }
}