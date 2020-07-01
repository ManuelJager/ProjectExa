using Exa.Audio.Music;
using System.Collections.Generic;
using UnityEngine;

namespace Exa.Audio
{
    public class AudioController : MonoBehaviour
    {
        public List<Sound> sounds;

        public MusicTrack ST_AudioTrack;
        public AudioTrack UI_SFX_AudioTrack;

        private Dictionary<string, Sound> soundById = new Dictionary<string, Sound>();

        public void Awake()
        {
            foreach (var sound in sounds)
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
            var sound = soundById[soundId];

            GetTrack(sound.audioType).PlayGlobal(sound);
        }

        public void Register(Sound sound)
        {
            soundById[sound.id] = sound;
            GetTrack(sound.audioType).Register(sound);
        }

        private AudioTrack GetTrack(AudioType audioType)
        {
            switch (audioType)
            {
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