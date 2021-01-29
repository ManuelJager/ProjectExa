using System.Collections.Generic;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio.Music
{
    [CreateAssetMenu(menuName = "Audio/TrackList")]
    public class DefaultSoundTrack : ScriptableObjectBag<Song>, ISoundTrack
    {
        public ISoundTrackDescription Description { get; private set; }

        private void OnEnable() {
            Description = new DefaultSoundTrackDescription(this);
        }

        IEnumerator<ISong> IEnumerable<ISong>.GetEnumerator() {
            return GetEnumerator();
        }
    }
}