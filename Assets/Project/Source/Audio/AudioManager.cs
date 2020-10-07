using Exa.Audio.Music;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public SoundBag soundBag;
        public MusicTrack stAudioTrack;
        public AudioTrack uiSfxAudioTrack;

        private readonly Dictionary<string, Sound> _soundById = new Dictionary<string, Sound>();

        private void Awake()
        {
            foreach (var sound in soundBag)
            {
                Register(sound);
            }
        }

        /// <summary>
        /// Play a sound with the given id
        /// </summary>
        /// <param name="soundId"></param>
        public void PlayGlobal(string soundId)
        {
            if (!_soundById.ContainsKey(soundId))
            {
                UnityEngine.Debug.LogError($"{soundId} doesn't exist");
                return;
            }

            var sound = _soundById[soundId];

            GetTrack(sound.audioType).PlayGlobal(sound);
        }

        public void Register(Sound sound)
        {
            _soundById[sound.id] = sound;
            GetTrack(sound.audioType).Register(sound);
        }

        private AudioTrack GetTrack(AudioType audioType)
        {
            switch (audioType)
            {
                case AudioType.St:
                    return stAudioTrack;

                case AudioType.UiSfx:
                    return uiSfxAudioTrack;

                default:
                    return null;
            }
        }
    }
}