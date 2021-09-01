using System.Collections;
using System.Collections.Generic;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Audio;

#pragma warning disable CS0649

namespace Exa.Audio {
    public class AudioPlayerGroup : MonoBehaviour, ITrackContext {
        [SerializeField] private string volumeKey;
        [SerializeField] private AudioMixerGroup audioMixerGroup;

        // Stores a handle group for the currently playing sounds of the given id
        protected SoundHandleGroupDictionary handles = new SoundHandleGroupDictionary();

        // Stores an audio source for sound on the track
        protected Dictionary<string, AudioSource> players = new Dictionary<string, AudioSource>();

        // Set the volume of an audio track using a 0-1 based float range
        public float Volume {
            set {
                // Convert linear to logarithmic
                var actualVolume = value > 0.001f
                    ? Mathf.Log(value) * 20
                    : -80;

                audioMixerGroup.audioMixer.SetFloat(volumeKey, actualVolume);
            }
        }

        public void RegisterHandle(SoundHandle soundHandle) {
            soundHandle.RegisterHandle(handles);
        }

        public void StopAllSounds() {
            handles.Stop();
        }

        /// <summary>
        ///     Plays an audio object on this track
        /// </summary>
        /// <param name="sound">Audio object to be played</param>
        /// <param name="progress">Progress</param>
        public SoundHandle PlayGlobal(ISound sound, float progress = 0f) {
            // Get a handle for the sound, play it, and add it to the collection
            var handle = GetGlobalHandle(sound);

            // Return the handle to the caller
            handle.Play(this, progress);

            return handle;
        }

        /// <summary>
        ///     Registers a sound on this track
        /// </summary>
        /// <param name="sound"></param>
        public void Register(ISound sound) {
            var go = new GameObject($"Player: {sound.Id}");
            go.transform.SetParent(transform);
            var source = go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = audioMixerGroup;
            players[sound.Id] = source;
            handles.RegisterGroup(sound.Id);
        }

        public void Clear() {
            players.Values.ForEach(player => player.gameObject.Destroy());
            players.Clear();
            handles.Clear();
        }

        /// <summary>
        ///     Generates the handle for the sound that needs to be played
        /// </summary>
        /// <param name="sound"></param>
        /// <returns></returns>
        private SoundHandle GetGlobalHandle(ISound sound) {
            var source = players[sound.Id];

            return new SoundHandle {
                audioSource = source,
                sound = sound
            };
        }
    }
}