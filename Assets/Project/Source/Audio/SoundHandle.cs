using System.Collections;
using Exa.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Exa.Audio {
    /// <summary>
    ///     Defines a handle for a single sound
    /// </summary>
    public class SoundHandle {
        public AudioSource audioSource;

        private ITrackContext context;

        /// <summary>
        ///     Is invoked when the sound ends or is stopped
        /// </summary>
        public UnityEvent onEnd = new UnityEvent();

        /// <summary>
        ///     Is invoked when the sound is manually stopped
        /// </summary>
        public UnityEvent onStop = new UnityEvent();
        public ISound sound;

        public void Play(ITrackContext playableContext, float progress) {
            context = playableContext;

            if (!sound.Config.allowMultipleOfType) {
                context.StopAllSounds();
            }

            if (sound.Config.oneShot) {
                audioSource.PlayOneShot(sound.AudioClip, sound.Config.volume);
            } else {
                audioSource.clip = sound.AudioClip;
                audioSource.volume = sound.Config.volume;
                audioSource.time = progress * sound.AudioClip.length;
                audioSource.Play();
            }

            audioSource.pitch = sound.Config.pitch;

            context.RegisterHandle(this);
        }

        public void Stop() {
            onStop?.Invoke();
            audioSource.Stop();
        }
        
        public void RegisterHandle(SoundHandleGroupDictionary handles) {
            // Remove the handle for the sound after finishing playing
            var endRoutine = WaitForSoundEnd(handles).Start(S.Audio);

            onStop.AddListener(
                () => {
                    handles.Remove(this);
                    onEnd.Invoke();
                    S.Audio.StopCoroutine(endRoutine);
                }
            );

            handles.Add(this);
        }
        
        /// <summary>
        ///     Waits for a sound to end,
        ///     assumes a sound
        /// </summary>
        /// <param name="handles"></param>
        /// <returns></returns>
        public IEnumerator WaitForSoundEnd(SoundHandleGroupDictionary handles) {
            // Wait for the sound to play
            yield return new WaitForSeconds(sound.AudioClip.length - audioSource.time);

            // Remove context from currently playing sounds
            handles.Remove(this);

            // Invoke the on end callback on the sound handle
            onEnd?.Invoke();
        }
    }
}