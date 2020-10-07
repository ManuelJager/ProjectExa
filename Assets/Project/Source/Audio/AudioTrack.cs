using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Exa.Audio
{
    public class AudioTrack : MonoBehaviour, ITrackContext
    {
        [SerializeField] private string _volumeKey;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;

        // Stores a handle group for the currently playing sounds of the given id
        protected SoundHandleGroupDictionary handleGroups = new SoundHandleGroupDictionary();

        // Stores an audio source for sound on the track
        protected Dictionary<string, AudioSource> players = new Dictionary<string, AudioSource>();

        // Set the volume of an audio track using a 0-1 based float range
        public float Volume
        {
            set
            {
                // Convert linear to logarithmic
                var actualVolume = value > 0.001f
                    ? Mathf.Log(value) * 20
                    : -80;

                _audioMixerGroup.audioMixer.SetFloat(_volumeKey, actualVolume);
            }
        }

        /// <summary>
        /// Plays an audio object on this track
        /// </summary>
        /// <param name="sound">Audio object to be played</param>
        public SoundHandle PlayGlobal(Sound sound)
        {
            // Get a handle for the sound, play it, and add it to the collection
            var handle = GetGlobalHandle(sound);

            // Return the handle to the caller
            handle.Play(this);

            return handle;
        }

        /// <summary>
        /// Registers a sound on this track
        /// </summary>
        /// <param name="sound"></param>
        public void Register(Sound sound)
        {
            var source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = _audioMixerGroup;
            players[sound.id] = source;

            handleGroups.RegisterGroup(sound.id);
        }

        public void RegisterHandle(SoundHandle handle)
        {
            // Remove the handle for the sound after finishing playing
            var endRoutine = Systems.Instance.StartCoroutine(WaitForSoundEnd(handle));

            handle.onStop.AddListener(() =>
            {
                handle.onEnd.Invoke();
                handleGroups.Remove(handle);
                StopCoroutine(endRoutine);
            });

            handleGroups.Add(handle);
        }

        public void StopAllSounds()
        {
            foreach (var group in handleGroups.Handles)
            {
                group.Stop();
            }
        }

        /// <summary>
        /// Generates the handle for the sound that needs to be played
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        private SoundHandle GetGlobalHandle(Sound sound)
        {
            var source = players[sound.id];

            var handle = new SoundHandle
            {
                audioSource = source,
                sound = sound
            };

            return handle;
        }

        /// <summary>
        /// Waits for a sound to end,
        /// assumes a sound
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        private IEnumerator WaitForSoundEnd(SoundHandle handle)
        {
            // Wait for the sound to play
            yield return new WaitForSeconds(handle.sound.audioClip.length - handle.audioSource.time);

            // Remove context from currently playing sounds
            handleGroups.Remove(handle);

            // Invoke the on end callback on the sound handle
            handle.onEnd?.Invoke();
        }
    }
}