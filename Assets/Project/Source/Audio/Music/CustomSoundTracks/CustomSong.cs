using UnityEngine;

namespace Exa.Audio.Music
{
    public class CustomSong : ISong
    {
        public AudioType AudioType => AudioType.ST;
        public string Id { get; }
        public AudioClip AudioClip { get; }
        public SoundConfig Config { get; }
        public Atmosphere AtmosphereFilter { get; }

        public CustomSong(AudioClip audioClip, CustomSongMetadata metadata) {
            Id = metadata.Name;
            AudioClip = audioClip;
            Config = new SoundConfig {
                volume = metadata.Volume,
            };
            AtmosphereFilter = metadata.Atmospheres;
        }
    }
}