using UnityEngine;

namespace Exa.Audio
{
    /// <summary>
    /// Defines a handle for a single sound
    /// </summary>
    public class SoundHandle
    {
        public AudioSource audioSource;
        public Sound audioObject;

        public void Play()
        {
            if (audioObject.allowMultipleOfType)
            {
                audioSource.PlayOneShot(audioObject.audioClip, audioObject.volume);
            }
            else
            {
                audioSource.clip = audioObject.audioClip;
                audioSource.volume = audioObject.volume;
                audioSource.Play();
            }
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
