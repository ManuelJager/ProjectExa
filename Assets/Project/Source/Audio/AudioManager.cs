﻿using System;
using Exa.Audio.Music;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public SoundBag soundBag;
        public MusicPlayerGroup ST_AudioTrack;
        public AudioPlayerGroup UI_SFX_AudioTrack;

        private readonly Dictionary<string, Sound> soundById = new Dictionary<string, Sound>();

        private void Awake() {
            foreach (var sound in soundBag) {
                Register(sound);
            }

            ST_AudioTrack.SetDefaultSoundtrack();
        }

        /// <summary>
        /// Play a sound with the given id
        /// </summary>
        /// <param name="soundId"></param>
        public void PlayGlobal(string soundId) {
            if (!soundById.ContainsKey(soundId)) {
                UnityEngine.Debug.LogError($"{soundId} doesn't exist");
                return;
            }

            PlayGlobal(soundById[soundId]);
        }

        public void PlayGlobal(Sound sound) {
            GetTrack(sound.AudioType).PlayGlobal(sound);
        }

        public void Register(Sound sound) {
            soundById[sound.Id] = sound;
            GetTrack(sound.AudioType).Register(sound);
        }
        
        private AudioPlayerGroup GetTrack(AudioType audioType) {
            return audioType switch {
                AudioType.ST => ST_AudioTrack,
                AudioType.UI_SFX => UI_SFX_AudioTrack,
                _ => throw new ArgumentException("Invalid audioType given", nameof(audioType))
            };
        }
    }
}