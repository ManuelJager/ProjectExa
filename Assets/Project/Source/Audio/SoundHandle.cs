using UnityEngine;
using UnityEngine.Events;

namespace Exa.Audio
{
    /// <summary>
    /// Defines a handle for a single sound
    /// </summary>
    public class SoundHandle
    {
        public AudioSource audioSource;
        public Sound sound;

        /// <summary>
        /// Is invoked when played
        /// </summary>
        public UnityEvent onPlay = new UnityEvent();

        /// <summary>
        /// Is invoked when the sound ends or is stopped
        /// </summary>
        public UnityEvent onEnd = new UnityEvent();

        /// <summary>
        /// Is invoked when the sound is manually stopped
        /// </summary>
        public UnityEvent onStop = new UnityEvent();

        private ITrackContext context;

        public void Play(ITrackContext playableContext)
        {
            context = playableContext;

            if (!sound.allowMultipleOnTrack)
            {
                context.StopAllSounds();
            }

            if (sound.allowMultipleOfType)
            {
                audioSource.PlayOneShot(sound.audioClip, sound.volume);
            }
            else
            {
                audioSource.clip = sound.audioClip;
                audioSource.volume = sound.volume;
                audioSource.Play();
            }

            audioSource.pitch = sound.pitch;

            context.RegisterHandle(this);
        }

        public void Stop()
        {
            onStop?.Invoke();
            audioSource.Stop();
        }
    }
}