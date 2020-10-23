using Exa.Audio.Music;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public SoundBag soundBag;
        public MusicTrack ST_AudioTrack;
        public AudioTrack UI_SFX_AudioTrack;

        private readonly Dictionary<string, Sound> soundById = new Dictionary<string, Sound>();

        private void Awake() {
            foreach (var sound in soundBag) {
                Register(sound);
            }
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

            var sound = soundById[soundId];

            GetTrack(sound.audioType).PlayGlobal(sound);
        }

        public void Register(Sound sound) {
            soundById[sound.id] = sound;
            GetTrack(sound.audioType).Register(sound);
        }

        private AudioTrack GetTrack(AudioType audioType) {
            switch (audioType) {
                case AudioType.ST:
                    return ST_AudioTrack;

                case AudioType.UI_SFX:
                    return UI_SFX_AudioTrack;

                default:
                    return null;
            }
        }
    }
}