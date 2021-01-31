using System;
using System.IO;
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

        public CustomSong(MemoryStream stream, CustomSongMetadata metadata) {
            Id = metadata.Name;
            AudioClip = GetAudioClip(stream, Path.GetExtension(metadata.FileName));
            Config = new SoundConfig();
            AtmosphereFilter = metadata.Atmospheres;
        }

        private AudioClip GetAudioClip(MemoryStream stream, string extension) {
            return extension switch {
                ".wav" => AudioClipUtils.GetWavAudioClip(stream, Id),
                _ => throw new ArgumentException($"{extension} is not a supported audio type", nameof(extension))
            };
        }
    }
}