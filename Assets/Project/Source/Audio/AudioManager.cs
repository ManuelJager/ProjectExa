using System;
using System.Collections.Generic;
using Exa.Audio.Music;
using UnityEngine;

namespace Exa.Audio {
    public class AudioManager : MonoBehaviour {
        [SerializeField] private SoundBag soundBag;
        [SerializeField] private AtmosphereTrigger defaultTrigger;
        [SerializeField] private MusicPlayerGroup ST_AudioTrack;
        [SerializeField] private AudioPlayerGroup UI_SFX_AudioTrack;

        private readonly Dictionary<string, Sound> soundById = new Dictionary<string, Sound>();

        public MusicPlayerGroup Music {
            get => ST_AudioTrack;
        }

        public AudioPlayerGroup Effects {
            get => UI_SFX_AudioTrack;
        }

        public void Init() {
            foreach (var sound in soundBag) {
                Register(sound);
            }
            
            defaultTrigger.gameObject.SetActive(true);
        }

        public void PlayGlobal(Sound sound) {
            GetTrack(sound.AudioType).PlayGlobal(sound);
        }

        public void Register(Sound sound) {
            soundById[sound.Id] = sound;
            GetTrack(sound.AudioType)?.Register(sound);
        }

        private AudioPlayerGroup GetTrack(AudioType audioType) {
            return audioType switch {
                AudioType.Soundtrack => ST_AudioTrack,
                AudioType.InterfaceSFX => UI_SFX_AudioTrack,
                AudioType.GameplaySFX => null,
                _ => throw new ArgumentException("Invalid audioType given", nameof(audioType))
            };
        }
    }
}