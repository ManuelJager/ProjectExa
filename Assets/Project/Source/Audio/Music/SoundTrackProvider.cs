using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio.Music
{
    public class SoundTrackProvider : MonoBehaviour
    {
        [SerializeField] private DefaultSoundTrack defaultSoundTrack;

        public IEnumerable<ISoundTrackDescription> GetSoundTrackDescriptions() {
            yield return new DefaultSoundTrackDescription(defaultSoundTrack);
        }
    }
}