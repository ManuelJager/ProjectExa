using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace Exa.Audio
{
    public class AudioTrack : MonoBehaviour
    {
        // Stores a handle group for the currently playing sounds of the given id
        private Dictionary<string, SoundHandleGroup> handles = new Dictionary<string, SoundHandleGroup>();

        // Stores an audio source for sound on the track 
        private Dictionary<string, AudioSource> players = new Dictionary<string, AudioSource>();

        /// <summary>
        /// Plays an audio object on this track
        /// </summary>
        /// <param name="sound">Audio object to be played</param>
        public SoundHandle PlayGlobal(Sound sound)
        {
            // If the sound doesnt allow multiple sounds to be played in the track, stop all current sounds
            if (!sound.allowMultipleOnTrack)
            {
                foreach (var currHandle in handles.Values)
                {
                    currHandle.Stop();
                }
            }

            // Get a handle for the sound, play it, and add it to the collection
            var handle = GetGlobalHandle(sound);
            handle.Play();
            handles[sound.id].Add(handle);

            // Remove the handle for the sound after finishing playing
            StartCoroutine(RemoveHandle(handle));

            // Return the handle to the caller
            return handle;
        }

        /// <summary>
        /// Registers a sound on this track
        /// </summary>
        /// <param name="sound"></param>
        public void Register(Sound sound)
        {
            players[sound.id] = gameObject.AddComponent<AudioSource>();
            handles[sound.id] = new SoundHandleGroup();
        }

        /// <summary>
        /// Generates the handle for the sound that needs to be played
        /// </summary>
        /// <param name="audioObject"></param>
        /// <returns></returns>
        private SoundHandle GetGlobalHandle(Sound audioObject)
        {
            var source = players[audioObject.id];

            var handle = new SoundHandle
            {
                audioSource = source,
                audioObject = audioObject
            };

            return handle;
        }

        private IEnumerator RemoveHandle(SoundHandle handle)
        {
            yield return new WaitForSeconds(handle.audioObject.audioClip.length);

            handles[handle.audioObject.id].Remove(handle);
        }
    }
}
