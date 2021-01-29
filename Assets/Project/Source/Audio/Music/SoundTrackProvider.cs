using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exa.Audio.Music
{
    public class SoundTrackProvider : MonoBehaviour
    {
        [SerializeField] private DefaultSoundTrack defaultSoundTrack;

        public ISoundTrack DefaultSoundTrack => defaultSoundTrack;

        public IEnumerable<ISoundTrackDescription> GetSoundTrackDescriptions() {
            yield return new DefaultSoundTrackDescription(defaultSoundTrack);
        }

        public ISoundTrack Find(string soundTrackName) {
            return GetSoundTrackDescriptions()
                .FirstOrDefault(description => description.Name == soundTrackName)
                ?.GetSoundTrack();
        }
    }
}