using System;
using System.Linq;
using Exa.Utils;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Audio.Music
{
    [Flags]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Atmosphere
    {
        None = 1 << 0,
        MainMenu = 1 << 1,
        Combat = 1 << 2,
        All = ~0
    }

    public class MusicPlayerGroup : AudioPlayerGroup
    {
        [SerializeField] private SoundTrackProvider soundTrackProvider;

        private Atmosphere activeAtmosphere = Atmosphere.None;
        private ISoundTrack currentSoundtrack;
        private SoundHandle soundHandle;

        public ISoundTrack CurrentSoundtrack {
            get => currentSoundtrack;
            set {
                if (value == currentSoundtrack) {
                    return;
                }

                soundHandle?.Stop();
                soundHandle = null;

                currentSoundtrack = value;

                Clear();
                value.ForEach(Register);
            }
        }

        public SoundTrackProvider Provider => soundTrackProvider;

        public void SetAtmosphere(Atmosphere atmosphere) {
            activeAtmosphere = atmosphere;

            soundHandle?.Stop();

            if (currentSoundtrack.SelectSong(atmosphere).IsNotNull(out var song)) {
                soundHandle = PlayGlobal(song);
            }
        }

        public void Play() {
            SetAtmosphere(activeAtmosphere);
        }
    }
}