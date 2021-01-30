using System;
using System.Collections.Generic;
using System.Linq;
using Exa.Types.Generics;
using UnityEngine;

namespace Exa.Audio.Music
{
    public class SoundTrackProvider : MonoBehaviour
    {
        [SerializeField] private DefaultSoundTrack defaultSoundTrack;

        public ISoundTrack DefaultSoundTrack => defaultSoundTrack;
        public LazyCache<IEnumerable<ISoundTrackDescription>> DescriptionsBackingCache;
        public IEnumerable<ISoundTrackDescription> Descriptions => DescriptionsBackingCache.Value;

        private void Awake() {
            DescriptionsBackingCache = new LazyCache<IEnumerable<ISoundTrackDescription>>(() => {
                return GetSoundTrackDescriptions().ToList();
            });
        }

        private IEnumerable<ISoundTrackDescription> GetSoundTrackDescriptions() {
            yield return new DefaultSoundTrackDescription(defaultSoundTrack);

            foreach (var file in IO.Tree.Root.CustomSoundTracks.GetFiles("*.zip")) {
                yield return new CustomSoundTrackDescription(file);
            }
        }

        public ISoundTrack Find(string soundTrackName) {
            if (string.IsNullOrEmpty(soundTrackName)) {
                return null;
            }

            return Descriptions
                .FirstOrDefault(description => description.Name == soundTrackName)
                ?.GetSoundTrack();
        }
    }
}