using UnityEngine;

namespace Exa.Audio {
    public class GlobalAudioPlayerProxy : MonoBehaviour {
        public void Play(string soundId) {
            Systems.Audio.PlayGlobal(soundId);
        }

        public void Play(Sound sound) {
            Systems.Audio.PlayGlobal(sound);
        }
    }
}