using System;
using System.Linq;
using Exa.Utils;
using JetBrains.Annotations;
using UnityEngine;

#pragma warning disable CS0649

namespace Exa.Audio.Music
{
    [Flags]
    public enum Atmosphere
    {
        None = 1 << 0,
        MainMenu = 1 << 1,
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
                currentSoundtrack = value;

                Clear();
                value.ForEach(Register);
            }
        }

        public SoundTrackProvider Provider => soundTrackProvider;

        public void SetAtmosphere(Atmosphere atmosphere) {
            if (activeAtmosphere == atmosphere) return;

            activeAtmosphere = atmosphere;

            soundHandle?.Stop();
            soundHandle = PlayGlobal(currentSoundtrack.SelectSong(atmosphere));
        }
    }
}